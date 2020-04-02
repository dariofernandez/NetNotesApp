
$(document).ready(function () {
    //Create jQuery timpicker
    $("#timepicker").timepicker();
    //Create jQueryUI datepicker
    $("#startdatepicker").datepicker();
    //Create jQueryUI datepicker
    $("#VisitDate").datepicker();

    alert('before calling get resources');
    //var sUrl = '@Url.Action("GetResourceList", "ViewNotes", new  { visitId = "123" }, Request.Url.Scheme)';
    //$.ajax({
    //    url: sUrl
    //}).done(function (data) {
    //    $.each(data, function (i, value) {
    //        $('#selectResource').append($('<option>').text(value).attr('value', value));
    //    });

    //});

});

$("#submit-button").click(function () {
    // On submit button click close dialog box
    $("#createForm").dialog("close");

    //Set inserted vlaues
    var name = $("#trainingname").val();
    var selectInstructor = $("#selectInstructor").val();
    var startdatepicker = $("#startdatepicker").val();
    var enddatepicker = $("#enddatepicker").val();
    var timepicker = $("#timepicker").val();
    var duration = $("#duration").val();

    //// Call Create action method
    //$.post('/Home/Create', { "name": name, "instructor": selectInstructor, "startdate": startdatepicker, "enddate": enddatepicker, "time": timepicker, "duration": duration },
    //    function () {
    //        alert("data is posted successfully");
    //        window.location.reload(true);

    //    });
});

