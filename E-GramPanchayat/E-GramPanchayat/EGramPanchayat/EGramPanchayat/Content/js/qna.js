$(document).ready(function () {
    getQNADetail(1);
});

function Pagevalue(e) {  
    getQNADetail(parseInt($(e).attr("page")));
};

function Save_data() {
    var data = {
        Url: $('#txtUrl').val(),
        Question: $('#txtQuestion').val(),
        Answer: $('#txtAnswer').val(),
        IsActive: $("#ddlStatus").val()
    }
    $.ajax({
        type: "POST",
        url: "/Admin/QNA.aspx/Save_QNA_Data",
        data: JSON.stringify({ pobj: data }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (result) {
            if (result.d == 'Session Expired') {
                location.href = '/'
            }
            else if (result.d == 'true') {
                swal("","Record saved succesfully!", "success");
                resetQNA_Data();
                getQNADetail(1);
            } else {
                swal("Oops!",result.d,"error");
            }
        },
        error: function () {
            alert("", "Oops! Something went wrong.Please try later.", "error");
        },
        failure: function () {
            alert("", "Oops! Something went wrong.Please try later.", "error");
        }
    });

}
function resetQNA_Data()
{
    $('#txtUrl').val('');
    $("#txtQuestion").val('');
    $("#txtAnswer").val('');
    $("#ddlStatus").val('');
    $('#btnsave').attr('onclick', "Save_data()", 'style', "text-align:centre;");
    $('#btnsave').html('Save');
}

function getQNADetail(PageIndex) {
    var data = {
        PageIndex: PageIndex,
        PageSize: $("#ddlPageSize").val(),
    }
    $.ajax({
        type: "POST",
        url: "/Admin/QNA.aspx/GetQNA_Data",
        data: JSON.stringify({ pobj: data }) ,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (response) {
            if (response.d != "false") {
                var xml = $.parseXML(response.d);
                var pager = $(xml).find("Table");
                var data= $(xml).find("Table1");
                var count = 1;
                var x = '';
                if (data.length > 0) {
                    $("#ddlPageSize").show();
                    $("#EmptyTable").hide();                   
                    $.each(data, function (i) {
                        var sts = $(this).find("IsActive").text();
                        if (sts == 1) {
                            sts = "Active";
                        }
                        else if (sts == 0) {
                            sts = "Inactive";
                        }
                        x += '<tr class="thead"><td class="Id text-center"  >' + Number(count)
                            /*+ '</td><td class="Name text-center" >' + $(this).find("AutoId").text()*/
                            + '</td><td class="Name text-center" >' + $(this).find("URL").text()
                            + '</td><td class="Name text-center" >' + $(this).find("Question").text()
                            + '</td><td class="Name text-center" >' + $(this).find("Answer").text()
                            + '</td><td class="Name text-center" > ' + sts
                            + '</td><td class="Name text-center" >' + $(this).find("CreatedBy").text()
                            + '</a></td><td class="Subject text-center"><a href="javascript:void(0)"><i style="font-size: 15px;" onclick="edit(' + $(this).find("autoID").text() + ')" class="fa fa-pencil"></i></a>&emsp;' + ''
                            + '<a href="javascript:void(0)"><i style="font-size: 15px;" onclick="deleteDocs(' + $(this).find("autoID").text() + ')" class="fa fa-trash"></i></a></td></tr>';
                        count = count + 1
                    });
                    $('#tblData').html(x);
                    $(this).find("Id").text();
                }
                else {
                    $("#EmptyTable").show();
                    $("#tblFBS").hide();
                    $("#ddlPageSize").hide();
                }

                $(".Pager").ASPSnippets_Pager({
                    ActiveCssClass: "current",
                    PagerCssClass: "pager",
                    PageIndex: parseInt(pager.find("PageIndex").text()),
                    PageSize: parseInt(pager.find("PageSize").text()),
                    RecordCount: parseInt(pager.find("RecordCount").text())
                });
            }

            else {
                location.href = '/admin/login-page';
            }
        },
        error: function (result) {
            swal("Error deleting!", "Please try again", "error");
        }

    });
};

function deleteDocs(Id) {
    swal({
        title: "Are you sure?",
        text: "You want to delete record.",
        icon: "warning",
        showCancelButton: true,
        closeOnClickOutside: false,
        buttons: {
            cancel: {
                text: "No, cancel.",
                value: null,
                visible: true,
                className: "btn-warning",
                closeModal: true,
            },
            confirm: {
                text: "Yes, delete it.",
                value: true,
                visible: true,
                className: "",
                closeModal: false
            }
        }
    }).then(function (isConfirm) {
        if (isConfirm) {
            deleteDoc(Id);
        }
    })
}
function deleteDoc(Id) {
    var data = {
        AutoId: Id,
    };

    $.ajax({
        type: "POST",
        url: "/Admin/QNA.aspx/removeQNA_Data",
        data: JSON.stringify({ pobj: data }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        beforeSend: function () {

        },
        complete: function () {

        },
        success: function (response) {
            if (response.d == "true") {
                swal("", "Record deleted succesfully!", "success");
                getQNADetail(1);
                resetQNA_Data();
            } else {
                swal("",response.d,"error");
            }

        },
        error: function (result) {
            swal("","Oops,record not deleted!!","");
        },
        failure: function (result) {
            swal("", 'Oops,some thing  wrong.Please try again.', "error");
        }
    });
}

function edit(Id) {
    var data = {
        AutoId: Id,
    };
    $.ajax({
        type: "POST",
        url: "/Admin/QNA.aspx/EditQNA_Data",
        data: JSON.stringify({ pobj: data }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        beforeSend: function () {
        },
        complete: function () {
        },
        success: function (response) {
            if (response.d != "false") {
                var xml = $.parseXML(response.d);
                var data = $(xml).find("Table");
                var x = '';
                if (data.length > 0) {
                    $.each(data, function (i) {
                        debugger;
                        $('#AutoId').val($(this).find("autoID").text());
                        $('#txtUrl').val($(this).find("URL").text());
                        $('#txtQuestion').val($(this).find("Question").text());
                        $('#txtAnswer').val($(this).find("Answer").text());
                        $('#ddlStatus').val($(this).find("IsActive").text());
                        $('#btnsave').removeAttr('onclick');
                        $('#btnsave').attr('onclick', "Update(" + $(this).find("AutoId").text() + ")", 'style', "text-align:centre;");
                        $('#btnsave').html('Update');
                    });
                }
                else {

                }
            }

        },
        error: function (result) {
            swal("", 'Oops,some thing  wrong.Please try again.', "error");
        }

    });
}
function Update() {
    var data = {
        
        AutoId: $('#AutoId').val(),
        Url: $('#txtUrl').val(),
        Question: $('#txtQuestion').val(),
        Answer: $('#txtAnswer').val(),
        IsActive: $("#ddlStatus").val()
    };
    $.ajax({
        type: "POST",
        url: "/Admin/QNA.aspx/UpdateQNA_Data",
        data: JSON.stringify({ pobj: data }),
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (response) {
            if (response.d == 'Session Expired') {
                location.href = '/'
            }
            else if (response.d == "true") {
                swal("", 'Record updated successfully!', "success");
                resetQNA_Data();
                getQNADetail(1);
            } else {
                swal("Oops!", response.d, "error");
            }

        },
        error: function (result) {
            swal("", 'Oops,some thing  wrong.Please try again.', "error");
        },

        failure: function (result) {
            swal("", 'Oops,some thing  wrong.Please try again.', "error");
        }
    });

}


