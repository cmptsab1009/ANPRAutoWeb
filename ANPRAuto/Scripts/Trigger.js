//START Common Variable Declaration 
var iCount = 1;
var IsSent = false;

var rowStart = "<tr id='/' class=''>";
var rowEnd = '</tr>';
var colStart = '<td id="colTime/">';
var colEnd = '</td>';
var ulStart = '<ul class="private-shot">';
var ulPlaneStart = '<ul>';
var ulEnd = '</ul>';
var liStart = '<li>';
var liEnd = '</li>';
var spanColTopStart = '<span id="countryName/" class="counry-name">';
var spanColBottomStart = '<span id="vehno/" class="veh-no">';
var spanFrontColourStart = '<span id="frontColour/" class="blue-back select-style">';
var spanBckGrndColourStart = '<span id=bckGrndColour/" class="blue-back select-style">';
var spanCatTextStart = '<span id="cat/" class="private-text">';
var spanVehTypeTextStart = '<span id="vehType/" class="private-text">';
var colGateStart = '<td id="colGate/">';
var br = "</br>";

var hiddenValues = '<span class="hide" id="remainingValues/" status/="" img1/="" img2/="" img3/="" frame/=""></span>';


var spanColEnd = '</span>';

//var img1 = '<img id="cam/SS1" src="" />';
//var img2 = '<img id="cam/SS2" src="" />';
//var img3 = '<img id="cam/SS3" src="" />';
var srcImage = "srcImage";
var anchorStart = '<a href="#" data-toggle="modal" data-target="#modal-img" onclick="/">';
var anchorEnd = '</a>';
var img1 = '<canvas id="cam/SS1" crossOrigin="Anonymous"></canvas>';
var img2 = '<canvas id="cam/SS2" crossOrigin="Anonymous"></canvas>';
var img3 = '<canvas id="cam/SS3" crossOrigin="Anonymous"></canvas>';
//END

//----------------START--------Initialize Motion Triggers
setInterval(CheckMotionTriggerCam1, 1000);
setInterval(CheckMotionTriggerCam2, 1000);
setInterval(CheckMotionTriggerCam3, 1000);
setInterval(CheckMotionTriggerCam4, 1000);

var myVar = setInterval(PostDataToSave, 10000);
//-----------------END-----------------------------------------

//---------------------POp up Image LOAD at Index
function LoadImage(myCanvas) {
    //console.log(myCanvas);
    var Pic = document.getElementById(myCanvas).toDataURL("image/png");
    $("#popUpImg").attr("src", Pic);
    // console.log(Pic);
    return true;
}

//---------------END-----------------

//-------------------START SAVE DAT----------------------
function PostDataToSave() {
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
}
//------------------END---------------------------------

function CheckMotionTriggerCam1() {
    try {
        var request = new XMLHttpRequest();

        request.open('GET', 'http://' + $("#cam1IP").attr("onlyIP") + '/trigger/gpiotrigger?getgpin&wfilter=1', true);
        request.onload = function () {

            // Begin accessing JSON data here
            var data = (this.response);

            if (request.status >= 200 && request.status < 400) {
                var result = this.response.split("mimetype")[0].split('=')[1];
                if (result == 1) {
                    var cCount = iCount; iCount++;
                    AddRowsIntoQueue(cCount, $("#cam1IP").attr("camName")).then(x => setTimeout(CaptureScreenShot("#cam" + cCount + "SS1", $("#cam1IP").attr("onlyIP"), cCount, true, 1)), 1000);

                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS2", $("#cam1IP").attr("onlyIP"), cCount, false, 2), 1000);
                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS3", $("#cam1IP").attr("onlyIP"), cCount, false, 3), 1000);
                    // $("#cam1SS").attr("src", $("#cam1IP").attr("src"));
                }
                document.getElementById("cam1Trigger").innerHTML = this.response.split("mimetype")[0].split('=')[1];
            } else {
                document.getElementById("cam1Trigger").innerHTML = "0";
            }
        }
        request.send();
    } catch (e) {
        console.log($("#cam1IP").attr("onlyIP") + "-->" + e);
    }


}

function CheckMotionTriggerCam2() {
    try {
        var request = new XMLHttpRequest();

        request.open('GET', 'http://' + $("#cam2IP").attr("onlyIP") + '/trigger/gpiotrigger?getgpin&wfilter=1', true);
        request.onload = function () {

            // Begin accessing JSON data here
            var data = (this.response);

            if (request.status >= 200 && request.status < 400) {
                var result = this.response.split("mimetype")[0].split('=')[1];
                if (result == 1) {
                    var cCount = iCount; iCount++;
                    //CaptureScreenShot("#cam2SS",$("#cam2IP").attr("onlyIP"));
                    AddRowsIntoQueue(cCount, $("#cam2IP").attr("camName")).then(x => setTimeout(CaptureScreenShot("#cam" + cCount + "SS1", $("#cam2IP").attr("onlyIP"), cCount, true, 1), 1000));

                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS2", $("#cam2IP").attr("onlyIP"), cCount, false, 2), 1000);
                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS3", $("#cam2IP").attr("onlyIP"), cCount, false, 3), 1000);
                }
                document.getElementById("cam2Trigger").innerHTML = this.response.split("mimetype")[0].split('=')[1];
                //$("#cam1SS").attr("src", $("#cam2IP").attr("onlyIP"));
            } else {
                document.getElementById("cam2Trigger").innerHTML = "0";
            }
        }
        request.send();
    } catch (e) {
        console.log($("#cam2IP").attr("onlyIP") + "-->" + e);
    }


}

function CheckMotionTriggerCam3() {
    try {
        var request = new XMLHttpRequest();

        request.open('GET', 'http://' + $("#cam3IP").attr("onlyIP") + '/trigger/gpiotrigger?getgpin&wfilter=1', true);
        request.onload = function () {

            // Begin accessing JSON data here
            var data = (this.response);

            if (request.status >= 200 && request.status < 400) {
                var result = this.response.split("mimetype")[0].split('=')[1];
                if (result == 1) {
                    // debugger;
                    // CaptureScreenShot("#cam3SS",$("#cam3IP").attr("onlyIP"));
                    var cCount = iCount; iCount++;

                    AddRowsIntoQueue(cCount, $("#cam3IP").attr("camName")).then(x => setTimeout(CaptureScreenShot("#cam" + cCount + "SS1", $("#cam3IP").attr("onlyIP"), cCount, true, 1), 1000));

                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS2", $("#cam3IP").attr("onlyIP"), cCount, false, 2), 1000);
                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS3", $("#cam3IP").attr("onlyIP"), cCount, false, 3), 1000);
                }
                document.getElementById("cam3Trigger").innerHTML = this.response.split("mimetype")[0].split('=')[1];
            } else {
                document.getElementById("cam3Trigger").innerHTML = "0";
            }
        }
        request.send();
    } catch (e) {
        console.log($("#cam3IP").attr("onlyIP") + "-->" + e);
    }


}

function CheckMotionTriggerCam4() {
    try {
        var request = new XMLHttpRequest();

        request.open('GET', 'http://' + $("#cam4IP").attr("onlyIP") + '/trigger/gpiotrigger?getgpin&wfilter=1', true);
        request.onload = function () {

            // Begin accessing JSON data here
            var data = (this.response);

            if (request.status >= 200 && request.status < 400) {
                var result = this.response.split("mimetype")[0].split('=')[1];
                if (result == 1) {
                    // CaptureScreenShot("#cam4SS",$("#cam4IP").attr("onlyIP"));
                    var cCount = iCount; iCount++;
                    AddRowsIntoQueue(cCount, $("#cam4IP").attr("camName")).then(x => setTimeout(CaptureScreenShot("#cam" + cCount + "SS1", $("#cam4IP").attr("onlyIP"), cCount, true, 1), 2000));

                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS2", $("#cam4IP").attr("onlyIP"), cCount, false, 2), 1000);
                    setTimeout(CaptureScreenShot("#cam" + cCount + "SS3", $("#cam4IP").attr("onlyIP"), cCount, false, 3), 1000);
                }
                document.getElementById("cam4Trigger").innerHTML = this.response.split("mimetype")[0].split('=')[1];
            } else {
                document.getElementById("cam4Trigger").innerHTML = "0";
            }
        }
        request.send();
    } catch (e) {
        console.log($("#cam4IP").attr("onlyIP") + "-->" + e);
    }


}

function CaptureScreenShot(Id, IP, rowId, isRequired, imgCount) {
    try {

        var src = 'http://' + IP + '/capture/scapture';
        var onlyId = Id.replace("#", "");
        try {

            // $(Id).attr("src", src);
            // GET THE IMAGE.
            var img = new Image();
            img.crossOrigin = "Anonymous";
            img.src = src;

            // WAIT TILL IMAGE IS LOADED.
            img.onload = function () {
                fill_canvas(img, onlyId).then(x => UploadPic(onlyId, rowId, isRequired, imgCount));       // FILL THE CANVAS WITH THE IMAGE.
            }
            // $(Id).src("data:image/png;base64," + base64);
            //var base64 = getBase64Image(document.getElementById(onlyId));
            //console.log(Id + " imgBase64" + base64); //+ " src-->" + $(Id).attr("src")
        } catch (e) {
            console.log("exception : " + e);
        }


    } catch (e) {
        console.log($("#cam3IP").attr("onlyIP") + "-->" + e);
    }


}

async function GetPicOCRImage(onlyId) {

}

async function AddRowsIntoQueue(iCount, Gate) {
    var row = rowStart.replace('/', iCount);
    //  row += colStart + iCount + colEnd; + new Date().toLocaleDateString() + " " 

    row += colStart.replace('/', iCount) + new Date().toLocaleTimeString() + colEnd;

    row += colStart;
    row += ulPlaneStart;
    row += liStart + spanColTopStart.replace('/', iCount) + spanColEnd + liEnd;//"Dubai" + Country Name
    row += liStart + spanColBottomStart.replace('/', iCount) + spanColEnd + liEnd;//+ "K5746" + Veichle No.
    row += ulEnd;
    row += colEnd;

    row += colStart;
    row += ulPlaneStart;
    row += liStart + spanFrontColourStart.replace('/', iCount) + br + liEnd; //"Blue" + Front Colour
    row += liStart + spanBckGrndColourStart.replace('/', iCount) + br + liEnd; // Background Coour
    row += ulEnd;
    row += colEnd;


    row += colGateStart.replace('/', iCount) + Gate + colEnd;

    row += colStart;
    row += ulPlaneStart;
    row += liStart + spanCatTextStart.replace('/', iCount) + liEnd; //"Private" + Veichle Category
    row += liStart + spanVehTypeTextStart.replace('/', iCount) + hiddenValues.replace('remainingValues/', 'remainingValues' + iCount).replace('status/', 'status' + iCount).replace('img1/', 'img1' + iCount).replace('img2/', 'img2' + iCount).replace('img3/', 'img3' + iCount).replace('frame/', 'frame' + iCount) + liEnd; // "Short" + Veichle Plate Type
    row += ulEnd;
    row += colEnd;

    row += colStart;

    // Add ThreeImages
    row += ulStart;
    var canvas1Id = "cam/SS1".replace('/', iCount);
    row += liStart;
    row += anchorStart.replace('/', "return LoadImage('" + canvas1Id + "')");
    row += img1.replace('/', iCount);
    row += anchorEnd;
    row += liEnd;

    var canvas2Id = "cam/SS2".replace('/', iCount);
    row += liStart;
    row += anchorStart.replace('/', "return LoadImage('" + canvas2Id + "')");
    row += img2.replace('/', iCount);
    row += anchorEnd;
    row += liEnd;

    var canvas3Id = "cam/SS3".replace('/', iCount);
    row += liStart;
    row += anchorStart.replace('/', "return LoadImage('" + canvas3Id + "')");
    row += img3.replace('/', iCount);
    row += anchorEnd;
    row += liEnd;

    row += ulEnd;

    row += colEnd;

    row += rowEnd;
    //debugger;
    // console.log(row);

    $("#tblQueue").prepend(row);
    $("#queueSizeCount").text(iCount);
    //  return true;

}

function getBase64Image(img) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
}

async function fill_canvas(img, onlyId) {
    // CREATE CANVAS CONTEXT.
    //console.log('fill_canvas ' + onlyId);
    img.crossOrigin = "Anonymous";
    var canvas = document.getElementById(onlyId);
    var ctx = canvas.getContext('2d');

    canvas.width = img.width;
    canvas.height = img.height;

    ctx.drawImage(img, 0, 0);       // DRAW THE IMAGE TO THE CANVAS.
}

async function UploadPic(myCanvas, rowId, isRequired, imgCount) {
    //if (IsSent == true) { return; }

    // Generate the image data
    IsSent = true;
    var Pic = document.getElementById(myCanvas).toDataURL("image/png");
    Pic = Pic.replace(/^data:image\/(png|jpg);base64,/, "");
    // console.log("Pic "+Pic);

    // Sending the image data to Server
    $.ajax({
        type: 'POST',
        url: '/Home/UploadPic',
        data: '{ "imageData" : "' + Pic + '","isRequired" : "' + isRequired + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (msg) {

            $("#remainingValues" + rowId).attr('img' + imgCount + rowId, msg.ImagePath);
            if (isRequired == true) {
                $("#" + rowId).addClass("true");
                if (msg.Status == 'NoPlate' || msg.Status == 'UnRecognized') {
                    console.log(msg.Status);
                    $("#countryName" + rowId).text('LPR Failed').addClass("red-back");
                    $("#remainingValues" + rowId).attr("status" + rowId, msg.Status);
                   // $("#status" + rowId).text(msg.Status);
                }
                else {
                    // set all the LPR Values to repective row table
                    $("#countryName" + rowId).text(msg.CountryCode);
                    $("#vehno" + rowId).text(msg.PlateNo);
                    $("#frontColour" + rowId).text(msg.PlateColor).attr("background-color", msg.PlateColor);
                    $("#bckGrndColour" + rowId).text(msg.BackgroundColor).attr("background-color", msg.BackgroundColor);
                    $("#cat" + rowId).text(msg.Category);
                    $("#vehType" + rowId).text(msg.VeichleType);
                    $("#status" + rowId).text(msg.Status);
                    $("#remainingValues" + rowId).attr("status" + rowId, msg.Status);
                    $("#Frame" + rowId).text(msg.Frame);
                }
            }
            //alert("Done, Picture Uploaded.");
        },
        error: function (e) {
            console.log(e);
        }
    });
}