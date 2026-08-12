
function internalPost(url, formdata, parentControl, property) {
    var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
    var headers = {};
    headers['__RequestVerificationToken'] = antiForgeryToken;

    $.ajax({
        type: "POST",
        url: url,
        headers: headers,
        data: formdata,
        datatype: 'application/json',
        success: function (data) {
            //console.log(data);
            if (data != (null || undefined)) {
                if (data.Invalid) {
                    emsg(data.Message, false);
                }
                else {
                    divbindwithSubproperties(data, parentControl, property);
                    //$("#hdnCustID").val(data.CustID);
                    //$("#hdnUserID").val(data.Userid);
                    // $('#DataListTable,#ADDForm').slideToggle();
                }
            }
        },
        error: function (xhr, error, status) {
            //debugger;
            var err = xhr;
        }
    });

}

function divbindwithSubproperties(data, parentControl, propety) {
    if (data != (null || undefined) && parentControl != (null || undefined)) {
        var childControls = $("#" + parentControl + " :input, #ImgCustomerCreation_Logo");
        if (childControls != (null || undefined)) {
            var childControlsArray = Enumerable.From(childControls)
                .Where(function (x) {
                    return $(x).attr("name") != "__RequestVerificationToken"
                        && $(x).attr("type") != "submit" && $(x).attr("type") != "button" && $(x).attr("type") != "reset"
                })
                .Select(function (s) { return s; }).ToArray();
            $.each(childControlsArray, function (c, i) {
                if (i != (null || undefined)) {
                    var ctrlType = $(i).attr("type");
                    var propArray;
                    if (ctrlType != 'hidden') {
                        propArray = $(i).attr("id").split('_');
                    }
                    else {
                        if ($(i).attr("cd") != (null || undefined)) {
                            propArray = $(i).attr("id").split('_');
                        }
                        else {
                            if ($(i).attr("name") != (null || undefined)) {
                                propArray = $(i).attr("name").split('.');
                            }
                        }
                    }
                    var propName = '';
                    if (propArray != (null || undefined)) {
                        propName = propArray.length == 2 ? propArray[1] : propArray[0];
                        var dataArray = Enumerable.From(data).Where(function (x) { return x.Key.toUpperCase() == propName.toUpperCase() }).FirstOrDefault();
                        switch (ctrlType) {
                            case 'text':
                            case 'textarea':
                            case 'password':
                            case 'number':
                                if (dataArray != (null || undefined))
                                    $("#" + propety + propName).val(dataArray.Value);
                                break;
                            //case 'textarea':
                            //    // debugger;
                            //    if (dataArray != (null || undefined))
                            //        $("#" + propety + propName).text(dataArray.Value);
                            //    break;
                            case 'checkbox':
                                if (dataArray != (null || undefined))
                                    $("#" + propety + propName).prop('checked', dataArray.Value);
                                break;
                            case 'select':
                                if (dataArray != (null || undefined)) {
                                    $("#" + propety + propName).val(dataArray.Value);
                                    //var ctr = $("#" + propety + propName + ", select")[2];
                                    var ctr = $("select[id$='" + propety + propName + "']");
                                    for (var i = 0; i < ctr[0].options.length; i++) {
                                        if (ctr[0].options[i].value == dataArray.Value) {
                                            ctr[0].options[i].selected = true;
                                            return;
                                        }
                                    }
                                }
                                break;
                            case 'radio':
                                if (dataArray != (null || undefined)) {
                                    if (dataArray.Value == true) {
                                        $("#" + propety + propName).prop('checked', 'checked');
                                    }
                                    else
                                        $("#" + propety + propName).removeProp('checked');
                                }
                                break;
                            case 'image':
                                if (dataArray.Value != (null || undefined)) {
                                    $("#ImgCustomerCreation_Logo").attr("src", dataArray.Value);
                                }
                                else {
                                    $("#ImgCustomerCreation_Logo").attr("src", "../Content/images/insert-img.png");
                                }
                                //$("#ImgCustomerCreation_Logo").attr("src", dataArray.Value)
                                break;
                            default:
                                if (ctrlType == 'hidden') {
                                    if ($(i).attr("cd") != (null || undefined)) {
                                        if (dataArray != (null || undefined))
                                            $("#" + propety + propName).val(dataArray.Value);
                                    }
                                    else {
                                        if (dataArray != (null || undefined))
                                            $("#" + propArray[0] + "." + propArray[1]).val(dataArray.Value);
                                    }
                                }
                                break;
                        }
                    }
                }
            });
        }
        return false;
    }
}

function connectiononedit(url, formdata, parentControl, property) {
    var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
    var headers = {};
    headers['__RequestVerificationToken'] = antiForgeryToken;

    $.ajax({
        type: "POST",
        url: url,
        headers: headers,
        data: formdata,
        datatype: 'application/json',
        success: function (data) {
            if (data != (null || undefined)) {
                if (data.Invalid) {
                    emsg(data.Message, false);
                    return false;
                } else {
                    connectionsbindwithSubproperties(data, parentControl, property);
                }
                // $('#DataListTable,#ADDForm').slideToggle();
            }
        },
        error: function (xhr, error, status) {
            //debugger;
        }
    });
}

function connectionsbindwithSubproperties(data, parentControl, propety) {
    //debugger;
    if (data != (null || undefined) && parentControl != (null || undefined)) {
        var childControls = $("#" + parentControl + " :input");
        if (childControls != (null || undefined)) {
            var childControlsArray = Enumerable.From(childControls)
                .Where(function (x) {
                    return $(x).attr("name") != "__RequestVerificationToken"
                        && $(x).attr("type") != "submit" && $(x).attr("type") != "button" && $(x).attr("type") != "reset"
                })
                .Select(function (s) { return s; }).ToArray();
            $.each(childControlsArray, function (c, i) {
                if (i != (null || undefined)) {
                    var ctrlType = $(i).attr("type");
                    var propArray;
                    if (ctrlType != 'hidden') {
                        propArray = $(i).attr("id").split('_');
                    }
                    else {
                        if ($(i).attr("cd") != (null || undefined)) {
                            propArray = $(i).attr("id").split('_');
                        }
                        else {
                            propArray = $(i).attr("name").split('.');
                        }
                    }
                    var propName = '';
                    propName = propArray.length == 2 ? propArray[1] : propArray[0];
                    var dataArray = Enumerable.From(data).Where(function (x) { return x.Key.toUpperCase() == propName.toUpperCase() }).FirstOrDefault();
                    switch (ctrlType) {
                        case 'text':
                        case 'textarea':
                        case 'password':
                        case 'number':
                            if (dataArray != (null || undefined))
                                $("#" + propety + propName).val(dataArray.Value);
                            break;
                        case 'textarea':
                            // debugger;
                            if (dataArray != (null || undefined))
                                $("#" + propety + propName).text(dataArray.Value);
                            break;
                        case 'checkbox':
                            if (dataArray != (null || undefined))
                                $("#" + propety + propName).prop('checked', dataArray.Value);
                            break;
                        case 'select':
                            //debugger;
                            if (dataArray != (null || undefined)) {

                                $("#" + propety + propName).val(dataArray.Value);
                                if (propName == "Instance") {
                                    $("#" + propety + propName + " option").removeAttr('selected', 'selected').prop('selected', false);
                                    $("#" + propety + propName + " option:contains('" + dataArray.Value + "')").attr('selected', 'selected').prop('selected', true);
                                }
                                //var ctr = $("#" + propety + propName + ", select")[2];
                                var ctr = $("select[id$='" + propety + propName + "']");
                                for (var i = 0; i < ctr[0].options.length; i++) {
                                    if (ctr[0].options[i].value == dataArray.Value) {
                                        ctr[0].options[i].selected = true;
                                        $("#" + propety + propName).trigger("change");
                                        return;
                                    }
                                }
                            }
                            break;
                        case 'radio':
                            if (dataArray != (null || undefined)) {
                                if (dataArray.Value == true) {
                                    $("#" + propety + propName).prop('checked', 'checked');
                                }
                                else
                                    $("#" + propety + propName).removeProp('checked');
                            }
                            break;
                        default:
                            if (ctrlType == 'hidden') {
                                if ($(i).attr("cd") != (null || undefined)) {
                                    if (dataArray != (null || undefined))
                                        $("#" + propety + propName).val(dataArray.Value);
                                }
                                else {
                                    if (dataArray != (null || undefined))
                                        $("#" + propArray[0] + "." + propArray[1]).val(dataArray.Value);
                                }
                            }
                            break;
                    }
                }
            });
        }
        return false;
    }
}

function clearparent(parentControl, propety) {
    if (parentControl != (null || undefined)) {
        var childControls = $("#" + parentControl + " :input");
        if (childControls != (null || undefined)) {
            var childControlsArray = Enumerable.From(childControls)
                .Where(function (x) {
                    return $(x).attr("name") != "__RequestVerificationToken"
                        && $(x).attr("type") != "submit" && $(x).attr("type") != "button" && $(x).attr("type") != "reset"
                })
                .Select(function (s) { return s; }).ToArray();
            $.each(childControlsArray, function (c, i) {
                if (i != (null || undefined)) {
                    var ctrlType = $(i).attr("type");
                    removeerrorstylectrolfocus(i);
                    var propArray; // = ctrlType != 'hidden' ? $(i).attr("id").split('_') : $(i).attr("name").split('.');
                    if (ctrlType != 'hidden') {
                        if ($(i).attr("id") != (null || undefined)) {
                            if ($(i).attr("id").indexOf('_') > 0) {
                                //if ($(i).attr("id").includes('_')){
                                propArray = $(i).attr("id").split('_');
                            } else {
                                propArray = $(i).attr("id");
                            }
                        }
                    } else {
                        if ($(i).attr("name") != undefined) {
                            if ($(i).attr("name").indexOf('.') > 0) {
                                propArray = $(i).attr("name").split('.');
                            } else {
                                propArray = $(i).attr("name");
                            }
                        }
                        else {
                            propArray = $(i).attr("name");
                        }
                    }
                    var propName = '';
                    if (propArray != (null || undefined)) {
                        propName = propArray.length == 2 ? propArray[1] : $.isArray(propArray[0]) ? propArray[0] : propArray;
                        switch (ctrlType) {
                            case 'text':
                            case 'textarea':
                            case 'hidden':
                            case 'password':
                                $("#" + propety + propName).val('');
                            case 'checkbox':
                                $("#" + propety + propName).prop('checked', false);
                            case 'select':
                                $("#" + propety + propName).prop('selectedIndex', 0);
                        }
                    }
                }
            });

        }
    }
}

function validateclick(parentctrl, btnctrl) {
    var IsValid = true;
    var childControls = Enumerable.From($(parentctrl).find("input[type=text],input[type=file], input[type=radio], input[type=checkbox],input[type=password], textarea, select"))
        .Where(function (x) { return $(x).attr("rc") === 'y' }).Select(function (s) { return s }).ToArray();
    if (childControls != (null || undefined)) {
        $.each(childControls, function (i, item) {
            var ctrType = $(item).attr("ct");
            switch (ctrType) {
                case "select":
                    if ($(item).val() == ('' || '0')) {
                        $(item).attr('style', 'border: 1px solid red !important');
                        $(item).insertAfter('<span style="color: red">please </span>');
                        IsValid = false;
                    }
                    break;
                case "checkbox":
                    $(item).attr('style', 'outline: 1px solid red !important');
                    IsValid = false;
                    break;
                case "radio":
                    if (!$(item).is(":checked")) {
                        $(item).attr('style', 'outline: red solid 1px; border: 1px solid red !important; border-image: none !important;');
                        IsValid = false;
                    }
                    break;
                case "password":
                    if ($(item).val() == '') {
                        $(item).attr('style', 'border: 1px solid red !important');
                        IsValid = false;
                    }
                    break;
                default:
                    if ($(item).val() == '') {
                        $(item).attr('style', 'border: 1px solid red !important');
                        IsValid = false;
                    }
                    break;
            }
        });
    }
    if (IsValid) {
        $("#" + btnctrl).click();
    }
}

function validateAjaxclick(parentctrl) {
    var IsValid = true;
    var childControls = Enumerable.From($(parentctrl).find("input[type=text],input[type=file], input[type=radio], input[type=checkbox],input[type=password], textarea, select"))
        .Where(function (x) { return $(x).attr("rc") === 'y' }).Select(function (s) { return s }).ToArray();
    if (childControls != (null || undefined)) {
        $.each(childControls, function (i, item) {
            var ctrType = $(item).attr("ct");
            switch (ctrType) {
                case "select":
                    if ($(item).val() == ('' || '0') || $(item).val() == null) {//condition added by G.Murali
                        $(item).attr('style', 'border: 1px solid red !important');
                        addspanerror(item);
                        IsValid = false;
                    }
                    break;
                case "checkbox":
                    $(item).attr('style', 'outline: 1px solid red !important');
                    addspanerror(item);
                    IsValid = false;
                    break;
                case "radio":
                    if (!$(item).is(":checked")) {
                        $(item).attr('style', 'outline: red solid 1px; border: 1px solid red !important; border-image: none !important;');
                        addspanerror(item);
                        IsValid = false;
                    }
                    break;
                case "password":
                    if ($(item).val() == '') {
                        $(item).attr('style', 'border: 1px solid red !important');
                        addspanerror(item);
                        IsValid = false;
                    }
                    break;
                case "file":
                    if ($(item).text() == '') {
                        $(item).attr('style', 'border: 1px solid red !important');
                        addspanerror(item);
                        IsValid = false;
                    }
                    break;
                case "email":
                    if ($(item).val() == '') {
                        $(item).attr('style', 'border: 1px solid red !important');
                        addspanerror(item);
                        IsValid = false;
                    }
                    if (item.value != "") {
                        var retval = true;
                        var reg = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,16}(?:\.[a-z]{2})?)$/i;

                        if (reg.test(item.value) == false) {
                            $(item).attr('style', 'border: 1px solid red !important');
                            addspanerror(item);
                            IsValid = false;
                        }

                        return retval;
                    }
                    break;
                default:
                    if ($(item).val() == '') {
                        $(item).attr('style', 'border: 1px solid red !important');
                        addspanerror(item);
                        IsValid = false;
                    }
                    break;
            }
        });
    }
    return IsValid;
}


function removeerrorstylectrolfocus(ctrl) {
    $(ctrl).attr('style', 'border: 1px solid #ccc');
    $(ctrl).parent().find(".error").remove();
}

function addspanerror(item) {
    $(item).parent().find(".error").remove();
    $(item).parent().append('<div class="error" style="color: red; height:10px !important;">' + $(item).attr("errormsg") + ' </div>');
}




function removeerrorstyleforallcontrols(parentctrl) {
    var childControls = Enumerable.From($(parentctrl).find("input[type=text],input[type=file], input[type=radio], input[type=checkbox],input[type=password], textarea, select"))
        .Where(function (x) { return $(x).attr("rc") === 'y' }).Select(function (s) { return s }).ToArray();
    if (childControls != (null || undefined)) {
        $.each(childControls, function (i, item) {
            var ctrType = $(item).attr("ct");
            switch (ctrType) {
                case "select":
                    $(item).attr('style', 'border: 1px solid #ccc !important');
                    break;
                case "checkbox":
                    $(item).attr('style', 'outline: 1px solid #ccc !important');
                    break;
                case "radio":
                    $(item).attr('style', 'outline: #ccc solid 1px; border: 1px solid #ccc !important; border-image: none !important;');
                    break;
                case "password":
                    $(item).attr('style', 'border: 1px solid #ccc !important');
                    break;
                default:
                    $(item).attr('style', 'border: 1px solid #ccc !important');
                    break;
            }
            removeerrorstylectrolfocus(item);
        });
    }
}
//Common Succes Meesage

function smsg(msg, isReload) {
    $("#message").removeClass("alert alert-danger");
    $("#message").addClass("alert alert-success");
    $("#message").html(msg);
    $("#message").show();
    setTimeout(function () {
        $("#message").fadeOut(3000);
        if (isReload) {
            window.location.reload(false);
        }
    }, 4000);
    $("html, body").animate({ scrollTop: 0 }, 600);
}

function TTsample() {
    $(document).trigger('nifty.ready');
    $(".dataTable").addClass("gridtable table-bordered");
    var table = $('.gridtable');

    table.dataTable({
        "destroy": true,
        "responsive": true,
        "order": [],
        "language": {
            "paginate": {
                "previous": '<i class="demo-psi-arrow-left"></i>',
                "next": '<i class="demo-psi-arrow-right"></i>'
            }
        },

        iDisplayLength: 10,
        aLengthMenu: [[10, 50, 100], [10, 50, 100,]],
        fnDrawCallback: function (o) {
            if (o._iDisplayLength == -2) {
            }
        }
    });
    table.css('width', '100%');
}

function campsmsg(msg, isReload, updateurl, reloadCtrl) {
    $("#message").removeClass("alert alert-danger");
    $("#message").addClass("alert alert-success");
    $("#message").html(msg);
    $("#message").show();
    setTimeout(function () {
        $("#message").fadeOut(3000);
        if (isReload) {
            //  window.location.reload(false);
            //  window.location = window.location;
            if (updateurl !== null)
                $("#" + reloadCtrl).load(updateurl);
            setTimeout(function () {
                TTsample();
            }, 500);
        }
        updateCredits();
    }, 4000);
    $("html, body").animate({ scrollTop: 0 }, 600);
}


//Common Failure Meesage
function emsg(msg, isReload) {
    $("#message").removeClass("alert alert-success");
    $("#message").addClass("alert alert-danger");
    $("#message").html(msg);
    $("#message").show();
    setTimeout(function () {
        $("#message").fadeOut(3000);
        if (isReload) {
            window.location.reload(false);
        }
    }, 4000);
    $("html, body").animate({ scrollTop: 0 }, 600);
}

function errorMessage(message) {
    $('.warningmessage').removeClass('warn-msg');
    $('.warningmessage').addClass('alert');
    $("#Department_IsValidDepartment").val('false');
    $(".warningmessage").addClass("alert alert-danger").html(message);
    $(".warningmessage").show();
    setTimeout(function () {
        $('.warningmessage').delay(800).fadeOut();
        $(".warningmessage").html('');
    }, 5000);
    $("html, body").animate({ scrollTop: 0 }, 600);
}

function successMessage(message) {
    $('.warningmessage').removeClass('warn-msg');
    $(".warningmessage").removeClass("alert alert-danger");
    $(".warningmessage").addClass("alert alert-success");
    $(".warningmessage").html(message);
    $(".warningmessage").show();
    setTimeout(function () {
        $('.warningmessage').delay(800).fadeOut();
        $(".warningmessage").html('');
        //  location.reload();
    }, 5000);
    $("html, body").animate({ scrollTop: 0 }, 600);
}

function activeorinactiverecord(url, formdata, inputText, anchor) {
    var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
    var headers = {};
    headers['__RequestVerificationToken'] = antiForgeryToken;
    var message = inputText.toLowerCase() == 'active' ?
        document.getElementById("hdnLang").value == "lngArbic" ? "هل أنت متأكد أن السجل نشط!" : "Are you sure active record!" :
        document.getElementById("hdnLang").value == "lngArbic" ? "هل أنت متأكد أن السجل غير نشط!" : "Are you sure in-active record!";
    var conf = confirm(message);
    if (conf == true) {
        $.ajax({
            type: "POST",
            url: url,
            headers: headers,
            data: formdata,
            datatype: 'application/json',
            success: function (data) {
                if (data != (null || undefined)) {
                    if (data.Invalid) {
                        errorMessage(data.Message, false);
                        return false;
                    }
                    if (data == 4) {
                        var acuadr = document.getElementById("hdnLang").value == "lngArbic" ? "مسار افتراضي آخر في حالة نشطة (مسار مفضل)." : "Another Default Route in active state(PreferedRoute).";
                        errorMessage(acuadr);
                        return false;
                    }
                    if (data == 12) {
                        var acuadr = document.getElementById("hdnLang").value == "lngArbic" ? "Selected Contact number cannot be activated as group is in Inactive status" : "Selected Contact number cannot be activated as group is in Inactive status.";
                        errorMessage(acuadr);
                        return false;
                    }
                    if (data) {
                        anchor.innerText = inputText;
                        var acus = document.getElementById("hdnLang").value == "lngArbic" ? "تم التحديث بنجاح" : "Updated Successfully";
                        successMessage(acus);
                        setTimeout(function () {
                            location.reload();
                        }, 5000);
                    } else {
                        var acfu = document.getElementById("hdnLang").value == "lngArbic" ? "فشل في التحديث." : "Failed to update.";
                        errorMessage(acfu);
                    }
                }
            },
            error: function (xhr, error, status) {
                var ace = document.getElementById("hdnLang").value == "lngArbic" ? "خطأ" : "error";
                errorMessage(ace);
            }
        });
    }
}

function AllowOnlyAlphaNumerics(e) {
    if ((e.which < 65 || e.which > 122) && (e.which < 48 || e.which > 57)) {
        e.preventDefault();
        return false;
    }
}

function PreventSpace(e) {
    if (e.which === 32) {
        e.preventDefault();
        return false;
    }
}

function periodOnchange(ctrl, txtstart, txtend) {
    var numberOfDaysToAdd = 0;
    switch (ctrl.value) {
        case "1":
            var start = new Date();
            $(txtstart).val(getFormattedDate(start));
            $(txtend).val(getFormattedDate(start));
            break;
        case "2":
            numberOfDaysToAdd = 1;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "3":
            numberOfDaysToAdd = 3;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "4":
            numberOfDaysToAdd = 7;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "5":
            numberOfDaysToAdd = 14;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "6":
            numberOfDaysToAdd = 30;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "7":
            var start = new Date();
            $(txtstart).val(getFormattedDate(start));
            $(txtend).val(getFormattedDate(start));
            break;
    }
}

function periodOnchangeusageanalysis(ctrl, txtstart, txtend) {
    var numberOfDaysToAdd = 0;
    switch (ctrl.value) {
        case "1":
            var start = new Date();
            $(txtstart).val(getFormattedDate(start));
            $(txtend).val(getFormattedDate(start));
            break;
        case "2":
            numberOfDaysToAdd = 1;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "3":
            numberOfDaysToAdd = 3;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "4":
            numberOfDaysToAdd = 7;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "5":
            numberOfDaysToAdd = 14;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "6":
            numberOfDaysToAdd = 30;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
        case "7":
            //var start = new Date();
            //$(txtstart).val(getFormattedDate(start));
            //$(txtend).val(getFormattedDate(start));
            //break;
            numberOfDaysToAdd = 1;
            var start = new Date();
            var startval = start.addDays(1);
            $(txtend).val(getFormattedDate(startval));
            var end = start.addDays(numberOfDaysToAdd);
            $(txtstart).val(getFormattedDate(end));
            break;
    }
}

function getFormattedDate(date) {
    var year = date.getFullYear();
    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;
    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;
    var hour = date.getHours().toString();
    hour = hour.length == 1 ? '0' + hour : hour;
    var minuts = date.getMinutes().toString();
    minuts = minuts.length == 1 ? '0' + minuts : minuts;
    var seconds = date.getSeconds().toString();
    seconds = seconds.length == 1 ? '0' + seconds : seconds;
    return month + '/' + day + '/' + year;// + ' ' + hour + ':' + minuts + ':' + seconds;
}

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() - days);
    return dat;
}



function validateInput1(input, kbEvent) {
    var keyCode, keyChar;
    //alert(kbEvent.keyCode);
    if (window.event)
        keyCode = kbEvent.keyCode; 	// IE
    else
        keyCode = kbEvent.which; 	//firefox            
    if (((keyCode == 8) || (keyCode == 32) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 97 && keyCode <= 122) || (keyCode == 45) || (keyCode == 95)))
        return true;
    else
        return false;
}

function activeStatusChange(url, id, ctrl) {
    var ascc = document.getElementById("hdnLang").value == "lngArbic" ? "هل تريد بالتأكيد المتابعة!" : "Are you sure want to proceed!";
    if (confirm(ascc) == true) {
        var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        var headers = {};
        headers['__RequestVerificationToken'] = antiForgeryToken;
        $.ajax({
            type: "POST",
            url: url,
            headers: headers,
            data: { id: id },
            datatype: 'application/json',
            success: function (d) {
                if (d.Invalid) {
                    emsg(d.Message, false);
                    return false;
                }
                else if (d) {
                    document.getElementById(ctrl).innerHTML = "Active";
                    //ctrl.innerHTML = "Active";
                    //(d.ctrl, false);
                    var asusi = document.getElementById("hdnLang").value == "lngArbic" ? "تم التحديث بنجاح" : "Updated Successfully";
                    successMessage(asusi);
                    setTimeout(function () {
                        location.reload();
                    }, 4000);
                } else {
                    document.getElementById(ctrl).innerHTML = "In-Active";
                    //ctrl.innerHTML = "In-Active";
                    var asuse = document.getElementById("hdnLang").value == "lngArbic" ? "تم التحديث بنجاح" : "Updated Successfully";
                    successMessage(asuse);
                    setTimeout(function () {
                        location.reload();
                    }, 4000);
                    //location.reload();
                }
            },
            error: function (xhr, error, status) {
                var err = xhr;
            }
        });
    } else {
        return false;
    }

}

function activeStatusChangerate(url, id, ctrl) {
    var ascr = document.getElementById("hdnLang").value == "lngArbic" ? "هل تريد بالتأكيد المتابعة!" : "Are you sure want to proceed!";
    if (confirm(ascr) == true) {
        var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        var headers = {};
        headers['__RequestVerificationToken'] = antiForgeryToken;
        $.ajax({
            type: "POST",
            url: url,
            headers: headers,
            data: { id: id },
            datatype: 'application/json',
            success: function (d) {
                if (d.Invalid) {
                    emsg(d.Message, false);
                    return false;
                }
                if (d) {

                    document.getElementById(ctrl).innerHTML = "Active";
                    //ctrl.innerHTML = "Active";
                    //(d.ctrl, false);
                    var ascri = document.getElementById("hdnLang").value == "lngArbic" ? "تم التحديث بنجاح" : "Updated Successfully";
                    successMessage(ascri);
                    //location.reload();
                } else if (d == false) {
                    document.getElementById(ctrl).innerHTML = "In-Active";
                    //ctrl.innerHTML = "In-Active";
                    var ascrf = document.getElementById("hdnLang").value == "lngArbic" ? "تم التحديث بنجاح" : "Updated Successfully";
                    successMessage(ascrf);
                    //location.reload();
                }
                setTimeout(function () {
                    window.location.reload(false);
                }, 5000);
            },
            error: function (xhr, error, status) {
                var err = xhr;
            }
        });
    } else {
        return false;
    }

}