
// Form-Component.js
// ====================================================================
// This file should not be included in your project.
// This is just a sample how to initialize plugins or components.
//
// - ThemeOn.net -


$(document).ready(function() {
	
	/* Keyword configuration */
	$('#demo-validfrom .input-group.date').datepicker({autoclose: true});
	$('#demo-validto .input-group.date').datepicker({autoclose:true});
	$('.basic-cal').datepicker({
		todayHighlight: true,
		autoclose:true
	});
	$('.dateranges .input-daterange').datepicker({
        format: "MM dd, yyyy",
        todayBtn: "linked",
        autoclose: true,
        todayHighlight: true
    });
	
/* Function to display current date in calendar */
var d = new Date();

var month = d.getMonth()+1;
var day = d.getDate();

var output = ((''+day).length<2 ? '0' : '') + day + '/' +
    ((''+month).length<2 ? '0' : '') + month + '/' +
    d.getFullYear();
	
	 $('.basic-cal').attr({
	     "placeholder": output,
	     format: "MM dd, yyyy",
	     todayBtn: "linked",
	     autoclose: true,
	     todayHighlight: true
	});
/* Function to display current date in calendar */
	
	$('.adv-cal').datepicker({autoclose:true});
	$('.basic-time').timepicker();
	
	$('.input-daterange').datepicker({
        format: "MM dd, yyyy",
        todayBtn: "linked",
        autoclose: true,
        todayHighlight: true
    });
	/* Keyword configuration */


    // CHOSEN
    // =================================================================
    // Require Chosen
    // http://harvesthq.github.io/chosen/
    // =================================================================
	$('#demo-chosen-select').chosen();
	$('.searchdropdown').chosen();
    $('.demo-cs-multiselect').chosen({width:'100%'});

    // BOOTSTRAP TIMEPICKER
    // =================================================================
    // Require Bootstrap Timepicker
    // http://jdewit.github.io/bootstrap-timepicker/
    // =================================================================
    $('#demo-tp-com').timepicker();



    // BOOTSTRAP TIMEPICKER - COMPONENT
    // =================================================================
    // Require Bootstrap Timepicker
    // http://jdewit.github.io/bootstrap-timepicker/
    // =================================================================
    $('#demo-tp-textinput').timepicker({
        minuteStep: 5,
        showInputs: false,
        disableFocus: true
    });


    // BOOTSTRAP DATEPICKER
    // =================================================================
    // Require Bootstrap Datepicker
    // http://eternicode.github.io/bootstrap-datepicker/
    // =================================================================
    $('#demo-dp-txtinput input').datepicker();


    // BOOTSTRAP DATEPICKER WITH AUTO CLOSE
    // =================================================================
    // Require Bootstrap Datepicker
    // http://eternicode.github.io/bootstrap-datepicker/
    // =================================================================
    $('#demo-dp-component .input-group.date').datepicker({autoclose:true});


    // BOOTSTRAP DATEPICKER WITH RANGE SELECTION
    // =================================================================
    // Require Bootstrap Datepicker
    // http://eternicode.github.io/bootstrap-datepicker/
    // =================================================================
    $('#demo-dp-range .input-daterange').datepicker({
        format: "MM dd, yyyy",
        todayBtn: "linked",
        autoclose: true,
        todayHighlight: true
    });


    // INLINE BOOTSTRAP DATEPICKER
    // =================================================================
    // Require Bootstrap Datepicker
    // http://eternicode.github.io/bootstrap-datepicker/
    // =================================================================
    $('#demo-dp-inline div').datepicker({
    format: "MM dd, yyyy",
    todayBtn: "linked",
    autoclose: true,
    todayHighlight: true
    });

});
