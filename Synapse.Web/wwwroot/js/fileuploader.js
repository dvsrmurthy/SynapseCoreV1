$(document).ready(function(){
	
		$("#fileupload").hide();
		$(".selectinfo").hide();
		$("#phbookgroup").hide();
		
	/*	$("#NotepadFile,#ExcelFile,#GroupFile").hover(function(){
			
		$(".selectinfo").fadeIn();
		},function(){
			$(".selectinfo").fadeOut();
		});
		
		
		$(".fa-file-text-o, .fa-file-excel-o, .fa-address-book-o").hover(function(){
			
		},function(){
			$(".selectinfo").fadeOut();
		});*/
		
			
	$("#NotepadFile,#ExcelFile,#GroupFile").hover(function(e)
		{
			$(".selectinfo").fadeIn().html("Select a db file type &nbsp;<i class='fa fa-arrow-right choosefile' ></i>");
		},function(){
			$(".selectinfo").fadeOut().html("Select a db file type &nbsp;<i class='fa fa-arrow-right choosefile' ></i>");
	});
		
		
		
function chgLbl() {


    if ($('i').hasClass('fa-times')) {
		$(".selectinfo").fadeIn().html("Close &nbsp;<i class='fa fa-arrow-right choosefile' aria-hidden='true'></i>");
		
		$("#NotepadFile,#ExcelFile,#GroupFile").hover(function(){
			 $(".selectinfo").fadeIn().html("Close &nbsp;<i class='fa fa-arrow-right choosefile' aria-hidden='true'></i>");
		},function(){
			$(".selectinfo").fadeOut().html("Close &nbsp;<i class='fa fa-arrow-right choosefile' aria-hidden='true'></i>");
		});

    } else {
		
		$("#NotepadFile,#ExcelFile,#GroupFile").hover(function(){
			  $(".selectinfo").fadeIn().html("Select a db file type &nbsp;<i class='fa fa-arrow-right choosefile' ></i>");
			 //$(".selectinfo").fadeIn().html("Close &nbsp;<i class='fa fa-arrow-right choosefile' aria-hidden='true'></i>");
		},function(){
			$(".selectinfo").fadeOut().html("Select a db file type &nbsp;<i class='fa fa-arrow-right choosefile' ></i>");
			//$(".selectinfo").fadeOut().html("Close &nbsp;<i class='fa fa-arrow-right choosefile' aria-hidden='true'></i>");
		});
      
    }
   
}
		
	
		$("#NotepadFile").click(function(){
			$("#fileupload").toggle().toggleClass("fupleftmove");
			$("#ExcelFile,#GroupFile,#HelpIcon").toggle();
			$(".jFiler-input-caption span").text("Choose text files .txt to Upload");
			$("#NotepadFile > i").toggleClass("fa-file-text-o");
			$("#NotepadFile > i").toggleClass("fa-times");
			$(".selectinfo").fadeOut();
			$("#phbookgroup").hide();
			chgLbl()
		});
		
		$("#ExcelFile").click(function(){
			$("#fileupload").toggle().toggleClass("fupleftmove");
			$("#NotepadFile,#GroupFile,#HelpIcon").toggle();
			$(".jFiler-input-caption span").text("Choose excel files .xls to Upload");
			$("#ExcelFile > i").toggleClass("fa-file-excel-o");
			$("#ExcelFile > i").toggleClass("fa-times");
			$(".selectinfo").fadeOut();
			$("#phbookgroup").hide();
			chgLbl()
		});
		
		$("#GroupFile").click(function(){
			$("#phbookgroup").toggle();
			$("#fileupload").hide();
			$("#NotepadFile,#ExcelFile,#HelpIcon").toggle();
			$(".jFiler-input-caption span").text("Choose group files to Upload");
			$("#GroupFile > i").toggleClass("fa-address-book-o");
			$("#GroupFile > i").toggleClass("fa-times");
			$(".selectinfo").fadeOut();
			chgLbl()
		});
			
		$('#contactsfile').filer({
			limit: 1,
			maxSize: 3,
			extensions: ["txt", "xls", "xlsx"],
			showThumbs: true,
			addMore: true,
			allowDuplicates: false,
			uploadFile:{
				url: null, //URL to which the request is sent {String}
				data: null, //Data to be sent to the server {Object}
				type: 'POST', //The type of request {String}
				enctype: 'multipart/form-data', //Request enctype {String}
				synchron: false, //Upload synchron the files
				beforeSend: null, //A pre-request callback function {Function}
				success: null, //A function to be called if the request succeeds {Function}
				error: null, //A function to be called if the request fails {Function}
				statusCode: null, //An object of numeric HTTP codes {Object}
				onProgress: null, //A function called while uploading file with progress percentage {Function}
				onComplete: null //A function called when all files were uploaded {Function}
			}
		});
		
});