$(document).ready(function () {

    // ─── KEY INPUT HANDLERS ───────────────────────────────────────────────────

    $('.ResetRld').on('click', function () {
        location.reload();
    });

    // Only allow specific special characters: @ # $ % * backspace
    $('.splchar').on('keypress', function (e) {
        var regex = new RegExp("^[@#$%*\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        return false;
    });

    // Only allow: & . _ - backspace
    $('.selsplchar').on('keypress', function (e) {
        var regex = new RegExp("^[&._-\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        return false;
    });

    // Restrict specific special characters: { } ! @ # $ % ^ & * ( ) , . < > / ? backspace
    $('.restrictsplchar').on('keypress', function (e) {
        var regex = new RegExp("^[{}!@#$%^&*(),.<>/?\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (!regex.test(str)) {
            return true;
        }
        return false;
    });

    // Restrict curly braces — allow only A-Z a-z 0-9 { } backspace
    $('.esccurly').on('keypress', function (e) {
        var regex = new RegExp("^[A-Za-z0-9{}\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (!regex.test(str)) {
            return true;
        }
        return false;
    });

    // Only allow date characters: 0-9 - : space backspace
    $('.onlydates').on('keypress', function (e) {
        var regex = new RegExp("^[0-9-:\\s\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        return false;
    });

    // Allow A-Z a-z 0-9 & . _ - space backspace (no Enter key)
    $('.sendidallowchr').on('keypress', function (e) {
        var regex = new RegExp("^[A-Za-z0-9&._-\\s\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str) && e.which !== 13) {
            return true;
        }
        return false;
    });

    // Only allow IP address characters: 0-9 . space backspace
    $('.onlyip').on('keypress', function (e) {
        var regex = new RegExp("^[0-9.\\s\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        return false;
    });

    // Only allow numbers: 0-9 space backspace
    $('.onlyNum').on('keypress', function (e) {
        var regex = new RegExp("^[0-9\\s\\b]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        return false;
    });

    // ─── SHOW / ADD FORM ─────────────────────────────────────────────────────

    $('.show-btn').on('click', function () {
        $('input[type="text"]').val('');        // NOTE: :text selector removed in jQuery 3 – use attribute selector
        $('input[type="radio"]').val('');
        $('input[type="radio"]').prop('disabled', false); // .removeAttr('disabled') deprecated for boolean attrs
        $('input[type="reset"],button[type="reset"],#Cleardetails').show();
    });

    // ─── FILE UPLOADER ───────────────────────────────────────────────────────

    // Show file name when a file is selected
    $('.uploader input[type="file"]').on('change', function (e) {
        var fileName = e.target.files[0].name;
        $(".filename").text(fileName);
        $("#btnImport").prop("disabled", true); // Use .prop() not .attr() for boolean attributes
    });

    // Clear file uploader on dropdown click
    $('#dropdownMenu1').on('click', function (e) {
        var input = $("#Contact_importFile");
        input.replaceWith(input.val('').clone(true));
        $("#ViewLog").hide();
        $(".filename").text("No file selected");
    });

    // ─── PAGE LOADING OVERLAY ────────────────────────────────────────────────

    $("#qLoverlay").fadeOut(250);
    $("#qLbar").fadeOut(250);

    // ─── IMPORT / ADD NUMBER TOGGLES ─────────────────────────────────────────

    $('#importbtn').hide();

    $('#ADDForm .addnumber input[type=radio]').on('click', function () {
        $('#ADDForm .import-div').fadeIn("slow").addClass('hide').removeClass('show');
        $('#ADDForm .add-div').fadeIn("slow").removeClass('hide');
        $('#importbtn').hide();
        $('#savedbtn, #clrbtn').show();
    });

    $('#ADDForm .importnumber input[type=radio]').on('click', function () {
        $('#ADDForm .add-div').fadeIn("slow").addClass('hide');
        $('#savedbtn, #clrbtn').hide();
        $('#importbtn').show();
        $('#ADDForm .import-div').fadeIn("slow").addClass('show');
    });

    $('#EDITForm .addnumber input[type=radio]').on('click', function () {
        $('#EDITForm .import-div').fadeIn("slow").addClass('hide').removeClass('show');
        $('#EDITForm .add-div').fadeIn("slow").removeClass('hide');
    });

    $('#EDITForm .importnumber input[type=radio]').on('click', function () {
        $('#EDITForm .add-div').fadeIn("slow").addClass('hide');
        $('#EDITForm .import-div').fadeIn("slow").addClass('show');
    });

    // ─── HOST / CUSTOMER SECTION ─────────────────────────────────────────────

    $('#HostFields,#customer-data').hide();
    $('.select-item option:nth-child(2),#Customer option').on('click', function () {
        $('#HostFields,#customer-data').fadeIn("slow").addClass('show');
    });

    // ─── SHOW / CLOSED BUTTON TOGGLE ─────────────────────────────────────────

    $('.closed-btn').hide();

    $('.show-btn').on('click', function (event) {
        sample("", "");
        $('.closed-btn').show();
        $('.show-btn').hide();
        $("#addusercontrols").show();
        $("#Importcontrols").hide();
        $("#SaveDetails").show();
        $("#ClearDetails").show();
        $("#btnImport").hide();
        $("#importbtn").hide();
    });

    // ─── ADD FORM OPEN ───────────────────────────────────────────────────────

    $('.show-btn').on('click', function (event) {
        $("#ADDForm #SaveDetails").val('Add');
        $("#ADDForm #btnSave").val('Add');
        $('#ADDForm .panel-heading h4 span').each(function () {
            var oldPhrase = $(this).text();
            var newPhrase = oldPhrase.replace('Edit', 'Add');
            $(this).text(newPhrase);
        });
    });

    // ─── EDIT FORM OPEN ──────────────────────────────────────────────────────

    $('.editform-btn').on('click', function (event) {
        $('.closed-btn').show();
        $('.show-btn').hide();
        $("#Importcontrols").hide();
        $("#addusercontrols").show();
        $("#SaveDetails").show();
        $("#ClearDetails").show();
        $("#btnImport").hide();
        $("#importbtn").hide();
        $("#ADDForm #SaveDetails").val('Save');
        $("#ADDForm #btnSave").val('Save');
        $('#ADDForm .panel-heading h4 span').each(function () {
            var oldPhrase = $(this).text();
            var newPhrase = oldPhrase.replace('Add', 'Edit');
            $(this).text(newPhrase);
        });
    });

    // ─── DASHBOARD VIEW SMSC ─────────────────────────────────────────────────

    $('#ViewFullDetails').hide();
    $('.ViewSMSC').on('click', function (event) {
        $('#ViewFullDetails .panel-heading').append('<a href="javascript:void(0)" class="btn btn-xs" style="color:#666; height:22px; text-indent:inherit; cursor:pointer; font-weight:bold">x</a>');
        $('.panel-heading a').on('click', function (event) {
            $('#DataListTable,#ViewFullDetails').slideToggle();
        });
    });

    $('.editforms-btn').on('click', function (event) {
        $('#ADDForm .panel-heading').append('<a href="javascript:void(0)" class="btn btn-xs editforms-btn" style="color:#666; height:22px; text-indent:inherit; cursor:pointer; font-weight:bold">x</a>');
        $('.panel-heading a').on('click', function (event) {
            $(".show-btn span").toggleClass("icomoon-icon-close icomoon-icon-plus iconred");
        });
        $('input[type="reset"],button[type="reset"],#Cleardetails').hide();
    });

    // ─── AUTOCOMPLETE STYLING ────────────────────────────────────────────────

    $('.ui-autocomplete').css({
        "height": "150px",
        "overflow-y": "scroll",
        "overflow-x": "hidden"
    });

    // ─── CHECKER SCRIPTS ─────────────────────────────────────────────────────

    $('#RejectNote').hide();

    $('.CheckerPendingBtn, #CloseFormBtnBtn, a.label-warning').on('click', function (event) {
        $("#CampaignTimings_RejectNote").attr('style', 'border: 1px solid #cccccc');
        $('#CheckerUpdation .ApproveBtn').show();
        $('.ApproveBtn').show();
        $(".EditFieldData").attr('style', 'border: 1px solid #CCCCCC');
        $('#RejectNote').hide();
    });

    $('a.label-warning').on('click', function () {
        $('.ApproveBtn').show();
        $('#RejectNote').hide();
        $('html,body').animate({
            scrollTop: $("body").offset().top
        }, 'slow');
    });

    $('.gridtable .fa-pencil-square-o, .dataTable .fa-pencil-square-o').on('click', function () {
        $('html,body').animate({
            scrollTop: $("body").offset().top
        }, 'slow');
        $('.panel-heading .panel-title').each(function () {
            var oldPhrase = $(this).text();
            var newPhrase = oldPhrase.replace('Add', 'Edit');
            $(this).text(newPhrase);
        });
    });

    $('#RejectNote, #CheckerUpdation .panel-footer').hide();

    $('.RejectBtn,#RejectCTT').on('click', function (event) {
        $("#CheckerUpdation #RejectNote textarea").prop('disabled', false); // Use .prop() not .removeAttr() for boolean attrs
        $(this).addClass('RejectClicked');
        if ($('#RejectNote').is(":visible") === false) {
            $("#CheckerUpdation #RejectNote textarea").attr('style', 'border: 1px solid #CCCCCC');
            $('#CheckerUpdation #RejectNote').fadeIn(2000, function () {
                $(this).show();
            });
        } else {
            if ($("#CheckerUpdation #RejectNote textarea").val() !== "") {
                $("#CheckerUpdation #RejectNote textarea").attr('style', 'border: 1px solid #CCCCCC');
            } else {
                $("#CheckerUpdation #RejectNote textarea").attr('style', 'border: 1px solid red !important');
                emsg('Please enter the reject reason.', false);
            }
        }
        $('#btnReset').removeClass('hide');
        $('#btnReset').fadeIn(500);
        $('#CheckerUpdation .EditFieldData').css("background", "#FAF6D2");
        $('#CheckerUpdation .ApproveBtn').fadeOut(2000, function () {
            $(this).hide();
        });
    });

    // ─── DATEPICKERS ─────────────────────────────────────────────────────────

    $('.setdate').datepicker({
        formate: "dd/MM/yy"   // intentional typo retained from original
    }).on('keydown', function (e) { e.preventDefault(); }); // .keydown(false) still works in 3.x but explicit is clearer

    (function () {
        $(".setdate-m-y").datepicker({
            changeMonth: true,
            changeYear: true
        }).on('keydown', function (e) { e.preventDefault(); });
    })();

    $(".MYdate").datepicker({
        format: "yyyy-mm",
        endDate: '0m',
        viewMode: "months",
        minViewMode: "months"
    }).on("changeDate", function (e) {        
        $(this).datepicker("hide");
    });

    // From / To date range
    var dateFormat = "dd/MM/yy";
    var from = $(".setdate-f")
        .datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 2
        })
        .on("change", function () {
            to.datepicker("option", "minDate", getDate(this));
        });

    var to = $(".setdate-t")
        .datepicker({
            defaultDate: "+1w",
            changeMonth: true,
            numberOfMonths: 2
        })
        .on("change", function () {
            alert('date changeed'); // original alert retained
            from.datepicker("option", "maxDate", getDate(this));
        });

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }
        return date;
    }

    $(".setdate-btn").datepicker({
        showButtonPanel: true
    }).on('keydown', function (e) { e.preventDefault(); });

    $('.cls-content #Email').trigger('focus'); // .focus(500) was not a valid jQuery call; use .trigger('focus')

    // ─── DATATABLE STYLING ───────────────────────────────────────────────────

    $(".dataTable").addClass("gridtable table-bordered");
    $('.dataTables_filter input, .dataTables_length select').addClass('form-control');
    $('#tblCampaignsWrapper').css('display', 'none');

    $('.syndate').datepicker({
        autoclose: true
    }).on('keydown', function (e) { e.preventDefault(); });

    // ─── PREVENT LEADING SPACE IN TEXT INPUTS ────────────────────────────────

    $("input[type=text]").on("keypress", function (e) {
        var keycode = e.which == null ? e.keyCode : e.which; // e.keycode → e.keyCode (case fix)
        if (keycode === 32 && !this.value.length) {
            e.preventDefault();
        }
    });

    // ─── PREVENT LEADING ZERO ────────────────────────────────────────────────

    // NOTE: .keyup("input propertychange paste", ...) was invalid — event name was being used as a string selector.
    // Correct usage: bind multiple events with .on()
    $(".zeronot").on("keyup input paste", function (e) {
        var val = $(this).val();
        var reg = /^0/gi;
        if (val.match(reg)) {
            $(this).val(val.replace(reg, ""));
            alert("First character should not be 0");
        }
    });

    // ─── $(window).load → replaced with $(window).on('load', ...) ────────────
    // NOTE: $(window).load() was removed in jQuery 3.0. Use $(window).on('load', fn) instead.
    $(window).on('load', function () {
        var r = $('<button type="reset" id="btnReset" class="btn btn-default hide">Close</button>');
        $("#CheckerUpdation .panel-footer, #CheckerUpdation .panel-footers").append(r);

        $("#AddForm #UserCreation_RoleId, #AddForm #UserCreation_CustomerId").chosen();

        $('#btnReset, #ResetBtn, #ClearBtn, #ClearTab1').on('click', function () {
            setTimeout(function () {
                $('#RejectNote').hide();
                $('#CheckerUpdation #btnReset').hide();
                $('#CheckerUpdation .ApproveBtn').show();
                $('#tbllength tbody').find('tr').remove();

                $('.panel-heading .panel-title').each(function () {
                    var oldPhrase = $(this).text();
                    var newPhrase = oldPhrase.replace('Edit', 'Add');
                    $(this).text(newPhrase);
                });

                $('option', $('.form-control')).each(function () {
                    $(this).prop('selected', false); // Use .prop() not .removeAttr('selected')
                });

                $('option', $('.form-controlotp')).each(function () {
                    $(this).attr('selected', 'selected');
                });

                $('.multiselect-native-select select').multiselect('refresh');

                $("input[type=checkbox]").each(function () {
                    this.checked = false;
                    $(this).prop('checked', false); // Use .prop() not .removeAttr('checked')
                });

                $('.dataTables_length select').trigger('change'); // .trigger() preferred over deprecated shortcuts
            }, 200);
        });
    });

    // ─── MULTISELECT DROPDOWN CLEANUP ────────────────────────────────────────

    $('.multiselect-native-select button').on('mousedown', function () {
        setTimeout(function () {
            $('.multiselect.dropdown-toggle').next('span').remove();
        }, 600);
    });

    // ─── DROPDOWN PROPAGATION ────────────────────────────────────────────────

    // NOTE: $(document).bind() was removed in jQuery 3.0. Use $(document).on() instead.
    $(document).on('click', function (e) {
        var $clicked = $(e.target);
        if (!$clicked.parents().hasClass("dropdowns")) $(".dropdowns dd ul").hide();
    });

    // ─── MULTI-SELECT CHECKBOX DROPDOWN ──────────────────────────────────────

    $('.mutliSelect input[type="checkbox"]').on('click', function () {
        var title = $(this).val() + ",";
        if ($(this).is(':checked')) {
            var html = '<span title="' + title + '">' + title + '</span>';
            $('.multiSel').append(html);
            $(".hida").hide();
        } else {
            $('span[title="' + title + '"]').remove();
            var ret = $(".hida");
            $('.dropdowns dt a').append(ret);
        }
    });

    // ─── REJECT BUTTON WIDTH FIX ─────────────────────────────────────────────

    // NOTE: getElementsByClassName returns an HTMLCollection, not a single element.
    // .innerHTML = "Reject" was being incorrectly assigned to the collection.
    // Replaced with a proper jQuery check.
    var rejwid = document.getElementsByClassName('btn-danger');
    if (rejwid.length) {
        setInterval(function () { $('.gridtable td .btn-danger').css('width', '64px'); }, 3);
    }

}); // end $(document).ready


// ─── GLOBAL HELPER FUNCTIONS ─────────────────────────────────────────────────

function hidemsg() {
    $("#warningmessage").fadeOut(4000);
}

function errormessage(msg) {
    $("#warningmessage").attr("class", "alert alert-danger");
    $("#warningmessage").html(msg);
    $("#warningmessage").show();
    hidemsg();
}

// Allow only alphanumeric characters (and backspace / tab)
function onlyAlphabets(e, t) {
    if (e.keyCode === 9) return;
    try {
        var charCode = e.which !== undefined ? e.which : e.keyCode; // window.event removed; use e directly
        if (
            (charCode > 64 && charCode < 91) ||
            (charCode > 96 && charCode < 123) ||
            (charCode >= 48 && charCode <= 57) ||
            e.keyCode === 8
        ) {
            return true;
        } else {
            return false;
        }
    } catch (err) {
        alert(err.AllowsAlphabetsOnly);
    }
}

// Email validation
function validateEmail(emailField) {
    var retval = true;
    var reg = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,16}(?:\.[a-z]{2})?)$/i;
    if (reg.test(emailField.value) === false) {
        $(emailField).parent().find(".error").remove();
        $(emailField).attr('style', 'border: 1px solid red !important');
        $(emailField).parent().append('<div class="error" style="color: red; height:10px !important;">Please Enter valid email</div>');
        retval = false;
        return retval;
    }
    return retval;
}

// Allow only numeric keys (for phone number inputs)
function isNumberKey(evt) {
    if (evt.keyCode === 9) return;
    var charCode = evt.which ? evt.which : evt.keyCode; // event.keyCode (global) replaced with evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) return false;
    return true;
}

// Allow decimal input
function isDecimalKey(evt, cntrl) {
    var found = $('#' + cntrl).val().indexOf('.');
    if (evt.keyCode === 9) return;
    var charCode = evt.which ? evt.which : evt.keyCode;
    if (charCode === 46 && found === -1) return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) return false;
    return true;
}

// Block all special characters
function blockSpecialChar(e) {
    if (e.keyCode === 9) return;
    var k = e.which ? e.which : e.keyCode; // document.all check removed (IE-only, not needed)
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k === 8 || k === 32 || (k >= 48 && k <= 57));
}

function getSelectedValue(id) {
    return $("#" + id).find("dt a span.value").html();
}

function Drponchange(value, id) {
    $("#" + id).val($(value).val());
}

function Chkonchange(value, id) {
    $("#" + id).val(value.checked);
}

function scrollTop() {
    $('html,body').animate({
        scrollTop: $("body").offset().top
    }, 'slow');
}

function oneditclk() {
    $('html,body').animate({
        scrollTop: $("body").offset().top
    }, 'slow');
    $('.closed-btn').show();
    $('.show-btn').hide();
    $("#Importcontrols").hide();
    $("#addusercontrols").show();
    $("#SaveDetails").show();
    $("#ClearDetails").show();
    $("#btnImport").hide();
    $("#importbtn").hide();
    $("#ADDForm #SaveDetails").val('Save');
    $("#ADDForm #btnSave").val('Save');
    $('#ADDForm .panel-heading h4 span').each(function () {
        var oldPhrase = $(this).text();
        var newPhrase = oldPhrase.replace('Add', 'Edit');
        $(this).text(newPhrase);
    });
}

function onchkeditclk() {
    $('html,body').animate({
        scrollTop: $("body").offset().top
    }, 'slow');
    $("#CampaignTimings_RejectNote").attr('style', 'border: 1px solid #cccccc');
    $('#CheckerUpdation .ApproveBtn').show();
    $('.ApproveBtn').show();
    $(".EditFieldData").attr('style', 'border: 1px solid #CCCCCC');
    $('input[type="reset"],button[type="reset"],#Cleardetails').hide();
    $('#RejectNote').hide();
}

function onchkviewclk() {
    $('.panel-heading a').on('click', function (event) {
        $('#DataListTable').show();
        $("#ViewForm").hide();
        $("#UserRouteEmailID").show();
        $(".btn-group").show();
    });
}

// SMS character / credit counter
function smscount() {
    var lang = 1;
    var intCount = 160;
    var divider1 = 153;
    var strCredits = 0;

    if ($(".arlng").is(":checked")) {
        lang = 2;
        intCount = 70;
        divider1 = 67;
    }

    var charcount = document.getElementById("QuicksmsorCampaign_Message").value.length;
    var msg = document.getElementById("QuicksmsorCampaign_Message").value;
    var GSMCHARS = $("#ExtGSMChars").val().split(',');

    for (var i = 0, n = msg.length; i < n; i++) {
        if (msg.charCodeAt(i) > 255) {
            lang = 2;
            break;
        }
    }

    if (lang === 1) {
        divider1 = 153;
        $.each(GSMCHARS, function (i, item) {
            charcount += msg.split(item).length - 1;
        });
        if (charcount <= 160) {
            strCredits = 1;
        } else if (charcount > 160 && charcount <= 306) {
            strCredits = 2;
        } else {
            var strTotal = charcount / divider1;
            strCredits = Math.ceil(strTotal);
        }
    } else if (lang === 2) {
        divider1 = 67;
        $.each(GSMCHARS, function (i, item) {
            charcount += msg.split(item).length - 1;
        });
        if (charcount <= 70) {
            strCredits = 1;
        } else if (charcount > 70 && charcount <= 134) {
            strCredits = 2;
        } else {
            var strTotal = charcount / divider1;
            strCredits = Math.ceil(strTotal);
        }
    }

    if (charcount === 0) { strCredits = 0; }
    $remaining.text(charcount + ' character(s),');
    $remaining.val(charcount);
    $QuicksmsorCampaign_Message.text(strCredits + ' SMS Message(s)');
    $QuicksmsorCampaign_Message.val(strCredits);

    if ($(".enlng").is(":checked") && lang === 2) {
        errorMessage("Message language did not match with the selected language.");
        $("html body").animate({ scrollTop: 0 });
    }
}

function numberWithThousands(x) {
    return x == null ? 0 : x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function specialcharecterserver() {
    var iChars = "!`@#$%^&*()+=-[]\\';,/{}|\":<>?~_";
    var data = document.getElementById("EmailtoSMS_Domain").value;
    for (var i = 0; i < data.length; i++) {
        if (iChars.indexOf(data.charAt(i)) !== -1) {
            errorMessage("Special characters are not allowed.");
            document.getElementById("EmailtoSMS_Domain").value = "";
            return false;
        }
    }
}

function specialcharecterserverset() {
    var iChars = "!`@#$%^&*()+=-[]\\';,/{}|\":<>?~_";
    var data = document.getElementById("MailServerSettings_Server").value;
    for (var i = 0; i < data.length; i++) {
        if (iChars.indexOf(data.charAt(i)) !== -1) {
            errorMessage("Special characters are not allowed.");
            document.getElementById("MailServerSettings_Server").value = "";
            return false;
        }
    }
}

function ValidateIPaddress(ipaddress) {
    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ipaddress)) {
        return true;
    }
    alert("You have entered an invalid IP address!");
    return false;
}
