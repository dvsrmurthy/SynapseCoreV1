$(document).ready(function() {

	
/* Message count */
        var $remaining = $('#remaining'),
        $txtMessage = $remaining.next();

        $('#txtMessage').keyup(function () {
            var chars = this.value.length,
                txtMessage = Math.ceil(chars / 160),
                remaining = txtMessage * 0 + (chars % (txtMessage * 160) || txtMessage * 160);

            $remaining.text(remaining + ' character(s),');
            $txtMessage.text(txtMessage + 'SMS Message(s)');
			//$("#TestSMSC_Message").val()=$(".phoneprview").val();
        });
		
		$("#txtMessage").change(function () {
			$(".phoneprview").val($('#txtMessage').val());
			/*if($("#txtMessage[direction='rtl']")==true)
			{
				alert("found rtl");
				$(".phoneprview").attr("direction","rtl");
			}*/
		});
		
		

/* End Message count */
	
$(".extdetails").hide();
$(".dataTable a").click(function(){
	$(".extdetails").slideToggle();
});

$(".dropdown-menu  li  a").click(function () {
	$("#langmenu > a:first-child").html($(this).text());
   // $(".dropdown > a:first-child").append(' &nbsp;<span class="caret" style="margin-top: -1px"></span>');
   // $(".btn:first-child").val($(this).text());

});

	
$("#filebuttons").hide();
/* $('input[type="file"]').change(function (e) {
	   var fileName = e.target.files[0].name;
	   $(".filename").text(fileName);
	   $("#filebuttons").show();
	   //alert('The file "' + fileName + '" has been selected.');
   });	*/					   
	
$("#rejectreason").hide();
$("#rejectbutton").click(function(){
	$("#rejectreason").fadeIn(2000);
	$("#acceptbutton").fadeOut(1000);
	$("#rejectreason textarea").css("background","#FAF6D2")
});

/* Quick sms panels show hide */	
//$("#RecpientDetails").hide();
$("#UploadExcelFile").hide();
$("#UploadTextFile").hide();
$("#UploadGroupFile").hide();
$("#messagefield").hide();
$("#campschedule").hide();
$("#phbookcontacts").hide();
$("#templatediv").hide();

$("#rdbSimpleSms").click(function(){
	$("#RecpientDetails").hide();
	$("#campschedule").hide();
});

$("#rdbCustomSms").click(function(){
	$("#RecpientDetails").show();
	$("#messagefield").show();
	$("#campschedule").show();
});

$("#rdbBulkSms").click(function(){
	$("#RecpientDetails").show();
	$("#messagefield").hide();
	$("#campschedule").show();
});




        $("#Excel").click(function (e) {
            e.preventDefault();
            $("#UploadExcelFile").show();
            $("#UploadTextFile").hide();
            $("#UploadGroupFile").hide();
            $("#btnCloseExcel").click(function () {
                $("#UploadExcelFile").hide();
            });
        });

        //$("#Notepad").click(function (e) {
        //    e.preventDefault();
        //    $("#UploadTextFile").show();
        //    $("#UploadExcelFile").hide();
        //    $("#UploadGroupFile").hide();
        //    $("#btnCloseText").click(function () {
        //        $("#UploadTextFile").hide();
        //    });
        //});

        //$("#Group").click(function (e) {
        //    e.preventDefault();
        //    $("#UploadGroupFile").show();
        //    $("#UploadTextFile").hide();
        //    $("#UploadExcelFile").hide();
        //    $("#btnCloseGroup").click(function () {
        //        $("#UploadGroupFile").hide();
        //    });
        //});
		
		$("#selectcontacts").click(function(){
			$("#phbookcontacts").show();
		});
		$("#closecontacts").click(function(){
			$("#phbookcontacts").hide();
		});
		
		$("#chkUseTemplate").click(function(){
			if($(this).prop("checked")==true)
			{
				$("#templatediv").show();
			}
			else{
				$("#templatediv").hide();
			}
		});
		
		
		
/* Quick sms panels show hide ends */

/* Contacts show hide */
$("#errmessage").hide();
$("#ImportContacts").hide();
$("#lnkContactDelete").click(function(){
	$("#errmessage").fadeIn().text("Please select one or more items to delete.");
});

$("#lnkImportContacts").click(function(){
	$("#ImportContacts").fadeIn();
});
$("#btnImportsClose").click(function(){
	$("#ImportContacts").fadeOut();
});
/* Contacts show hide ends */
	
//$('.dataTable').dataTable( {
//        "responsive": true,
//        "language": {
//            "paginate": {
//              "previous": '<i class="demo-psi-arrow-left"></i>',
//              "next": '<i class="demo-psi-arrow-right"></i>'
//            }
//        }
//    });
	
/*User Creation*/
$("#passwordControls").hide();
$("#Synapse-User").click(function(){
	$("#btnVerify").hide();
	$("#passwordControls").show();
});
$("#Ldap-User").click(function(){
	$("#btnVerify").show();
	$("#passwordControls").hide();
});
/*User Creation*/

/* Filter Words */
$("#importFilterWords").hide();
$("#rdbAddFilterWords").click(function(){
	$("#addFilterWords").show();
	$("#importFilterWords").hide();
});
$("#rdbImportFilterWords").click(function(){
	$("#addFilterWords").hide();
	$("#importFilterWords").show();
});
/* Filter Words */


/*Mobile lenth*/
$("#mobilelenth-table").hide();
$("#mobile-length").click(function(){
	$("#mobilelenth-table").show();							   							  
 });
/*end Mobile lenth*/

/* User Statistics */
$("#userdropdown").hide();
$("#userstatistics").hide();
$("#ddlCustomer").change(function(){
	$("#userdropdown").show();
});
$("#ddlUser").change(function(){
	$("#userstatistics").show();
});
/* End User Statistics */

/* Vendor Master */
$(".infotable").hide();
$("#btnAdd").click(function(){
	$(".infotable").show();
});
/* End Vendor Master */

/* Mailbox user mappings */
$("#tagspanel").hide();
$("#rdbTags").click(function(){
	$("#tagspanel").toggle();
});
/* Mailbox user mappings */

/* Template Creation */
$("#messagefields").hide();
$("#rdbDynamic").click(function(){
	$("#messagefields").show();
});
$("#rdbStatic").click(function(){
	$("#messagefields").hide();
});
/* End Template Creation */




/* formfeald placeholder */
$('input,textarea').focus(function(){
   $(this).data('placeholder',$(this).attr('placeholder'))
          .attr('placeholder','');
}).blur(function(){
   $(this).attr('placeholder',$(this).data('placeholder'));
});
/* end formfeald placeholder */

/* User mappings */
$("#templatetypes").hide();
$("#usertypes").hide();
$("#txtTemplateName").click(function(){
	$("#templatetypes").show();
});
$("#txtUser").click(function(){
	$("#usertypes").show();
});

/* End User mappings */

/* bulk sms schedule times */
$("#scheduleTime").hide();
$("#rdbSchedulelater").click(function(){
	$("#scheduleTime").show();
});
$("#rdbSendnow").click(function(){
	$("#scheduleTime").hide();
});
/* bulk sms schedule times */

/* bulk sms Language */
$("#rdbEnglish").click(function(){
	$("#txtMessage").css("direction","ltr");
});
$("#rdbArabic").click(function(){
	$("#txtMessage").css("direction","rtl");
});
/* bulk sms Language */


/* Popover */

		 $('.maininfo').popover({ 
			html : true,
			content: function() {
			  return $('#myPopoverContent').html();
			}
		  });
  
  
		$(document).on('click', function (e) {
			$('[data-toggle="popover"],[data-original-title]').each(function () {
				//the 'is' for buttons that trigger popups
				//the 'has' for icons within a button that triggers a popup
				if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {                
					(($(this).popover('hide').data('bs.popover')||{}).inState||{}).click = false  // fix for BS 3.3.6
				}
		
			});
		});
		
/* Popover */


/*$('input[type="checkbox"]').click(function(){
if($(this). prop("checked") == true){
alert("Checkbox is checked." );
}
});*/

$(".dataTable td .fa-pencil-square-o").click(function() {
    $('html, body').animate({
        scrollTop: $("#content-container").offset().top
    }, 900);
});

}); // End ready


