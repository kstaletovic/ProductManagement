(function (app, $) {
    function init() {
        app.product.index = this;
        var localizer = {};
        var dataTable = {};
        var btnDeleteModal = $("#btnDeleteModal");

        function initDataTable() {
            dataTable = $("#productDataTable").DataTable({
                autoWidth: false,
                searching: false,
                ajax: {
                    url: '/Product/GetDataTable',
                    type: 'POST',
                },
                rowId: 'productId',
                columnDefs: [
                    { targets: [0], data: 'productId' },
                    { targets: [1], data: 'productName' },
                    { targets: [2], data: 'productDescription' },
                    { targets: [3], data: 'productCategory' },
                    { targets: [4], data: 'productManufacturer' },
                    { targets: [5], data: 'productSupplier' },
                    { targets: [6], data: 'productPrice' },
                    {
                        targets: [7], className: 'text-center', orderable: false,
                        mData: function (d) {
                            var html = '<a href="Product/Edit?id=' + d.productId + '" class="btn btn-primary btn-sm">' + localizer.btnEdit + '</a> '
                            html += '<button class="btnDelete btn btn-danger btn-sm" data-product-id="' + d.productId + '">' + localizer.btnDelete + '</button>';
                            return html;
                        }
                    }
                ]
            });
        }

        function initClickEvents() {
            $(document).on('click', '.btnDelete', function () {
                $("#selectedProduct").val($(this).data('product-id'));
                $("#deleteModal").modal('show');
            });

            btnDeleteModal.click(function () {
                app.disableButton(btnDeleteModal);
                $.ajax({
                    url: "/Product/Delete",
                    type: "POST",
                    data: { id: $("#selectedProduct").val() },
                    success: function (response) {
                        $('#deleteModal').modal('hide');
                        dataTable.ajax.reload();
                    }
                }).always(function () {
                    app.enableButton(btnDeleteModal);
                });
            });
        }

        this.init = function (obj) {
            localizer = obj.localizer;
            initDataTable();
            initClickEvents();
        };
    }
    return new init();
})(app, jQuery);