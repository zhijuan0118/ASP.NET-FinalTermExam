
$(document).ready(function () {
            //員工名稱
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


            //日期
            $("#CreationDate").kendoDatePicker({
                format: "yyyy/MM/dd",
                parseFormats: ["yyyy/MM/dd"]
            });


            //刪除按鈕
            $("#BackBtn").kendoButton({
                click: function () {
                    window.location.href = 'Index';
                }
            })

            //存檔按鈕(送出表單)
            $("#InsertBtn").kendoButton({
                click: function () {
                    SetOrderDetials();
                    var validator = $("#Form").data("kendoValidator");
                    if (validator.validate()) {
                        $.ajax({
                            type: "POST",
                            url: "../Customer/DoInsertCustomer",
                            data: $("#Form").serialize(),
                            success: function (response) {
                                alert("Insert Success! CustomerId : " + response);
                                window.location.href = 'Index';
                            }
                        });
                    }
                }
            })

            //驗證
            var container = $("#Form");
            kendo.init(container);
            container.kendoValidator({
                rules: {
                    greaterdate: function (input) {
                        if (input.is("[data-greaterdate-msg]") && input.val() != "") {
                            var date = kendo.parseDate(input.val()),
                                otherDate = kendo.parseDate($("[name='" + input.data("greaterdateField") + "']").val());
                            return otherDate == null || otherDate.getTime() < date.getTime();
                        }

                        return true;
                    }
                }
            });

        })
    });
});