$(document).ready(function () {
    $(function () {
        $.getScript("../Scripts/kendo.all.min.js", function () {

            //查詢結果grid
            $("#grid").kendoGrid({
                dataSource: {
                    transport: {
                        read: {
                            url: "../Customer/Read",
                            dataType: "json",
                        },
                    },
                    pageSize: 20
                },
                columns: [{
                    field: "CustomerId",
                    title: "客戶編號",
                    width: "50px"
                }, {
                    field: "CompanyName",
                    title: "客戶名稱",
                    width: "100px"
                }, {
                    field: "ContactName",
                    title: "聯絡人姓名",
                    width: "100px"
                }, {
                    field: "ContactTitle",
                    title: "聯絡人職稱",
                    width: "100px"
                },
                {
                    command: [{
                        name: "Update",
                        click: UpdatCustomer
                    }, {
                        name: "Delete",
                        click: DeleteCustomer
                    }], title: "&nbsp;", width: "100px"
                }
                ],
                pageable: {
                    pageSizes: true,
                    buttonCount: 5
                },
                editable: false,
                sortable: true,
            });

            //員工資料
            $("#ContactTitle").kendoComboBox({
                dataTextField: "Text",
                dataValueField: "Value",
                dataSource: {
                    transport: {
                        read: "../Customer/GetContactTitleList",
                        dataType: "json",
                    },
                    schema: {
                        model: {
                            fields: {
                                Value: { type: "string" },
                                Text: { type: "string" },
                            }
                        }
                    },
                    pageSize: 80,
                    serverPaging: true,
                    serverFiltering: true
                }
            });


            //搜尋按鈕 送出表單 改變Grid資料
            $("#SearchBtn").kendoButton({
                click: function () {
                    $.ajax({
                        type: "POST",
                        url: "../Customer/Read",
                        data: $("#Form").serialize(),
                        success: function (response) {
                            var dataSource = new kendo.data.DataSource({
                                data: response,
                                pageSize: 20
                            });
                            var grid = $("#grid").data("kendoGrid");
                            grid.setDataSource(dataSource);
                        }
                    });
                }

            });


            //按鈕設置
            $("#ResetBtn").kendoButton();
            $("#InsertBtn").kendoButton({
                click: function () {
                    console.log("click");
                    window.location.href = 'InsertCustomer';
                }
            });

            //修改
            function UpdatCustomer(e) {
                var tr = e.currentTarget.closest('tr');
                var dataItem = this.dataItem(tr);
                var customerId = dataItem.CustomerId;
                console.log(customerId)
                window.location.href = 'UpdateOrder?CustomerId=' + customerId;
            }


            //刪除資料
            function DeleteCustomer(e) {
                var tr = e.currentTarget.closest('tr');
                var dataItem = this.dataItem(tr);
                console.log(dataItem.CustomerId);
                $.ajax({
                    type: "POST",
                    url: "/Customer/DeleteCustomer",
                    data: {
                        "CustomerId": dataItem.CustomerId
                    },
                    dataType: "json",
                    success: function (response) {
                        if (response) {
                            alert("客戶 " + dataItem.CustomerId + " 刪除成功")
                            var grid = $("#grid").data("kendoGrid");
                            grid.dataSource.remove(dataItem);
                            tr.remove();
                        } else {

                            alert("客戶 " + dataItem.CustomerId + " 刪除失敗")
                        }
                    }
                });
            }
        })
    });
});