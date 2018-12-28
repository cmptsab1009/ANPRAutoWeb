var myVar = setInterval(myTimer, 1000);




function myTimer() {
    var d = new Date();
    document.getElementById("date").innerHTML = d;
  //  document.getElementById("time").innerHTML = d.toLocaleTimeString();
}

function PostDataToSaveOutDated()
{
    var anprDataArray = [];
    var tabledata = $("#tblQueue").find("tr.true");
   // $("#tblQueue").html('');
    tabledata.each(function () {
        var idNo = $(this).attr("id");
        var anprData = {};
        anprData.Time = $("#colTime" + idNo).text();
        anprData.Lane = $("#colGate" + idNo).text();
        anprData.PlateNo = $("#vehno" + idNo).text();
        anprData.CountryCode = $("#countryName" + idNo).text();
        anprData.Status = $("#remainingValues" + idNo).attr("status" + idNo);
        anprData.PlateColour = $("#frontColour" + idNo).text();
        anprData.BackGroundColour = $("#bckGrndColour" + idNo).text();
        anprData.Category = $("#cat" + idNo).text();
        anprData.PlateSize = $("#vehType" + idNo).text();
        anprData.Image1 = $("#remainingValues" + idNo).attr("img1" + idNo);
        anprData.Image2 = $("#remainingValues" + idNo).attr("img2" + idNo);
        anprData.Image3 = $("#remainingValues" + idNo).attr("img3" + idNo);
        anprData.FrameStr = $("#remainingValues" + idNo).attr("frame" + idNo);
        anprDataArray.push(anprData);
        $(this).remove();
        iCount = iCount - 1;
        $("#queueSizeCount").text(iCount);
       // console.log(anprData);
    });

    $.ajax({
        type: 'POST',
        url: '/Home/SaveData',
        data: '{ "lstData" : ' + JSON.stringify(anprDataArray) + ' }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {
            console.log(msg);
        },
        error: function (e) {
            console.log(e);
        }
    });
    //console.log(JSON.stringify(anprDataArray));
}