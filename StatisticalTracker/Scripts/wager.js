


//function UploadFiles() {
    
//    var uploadFilesArray = [];
//    $.each($("#uploadFile").prop('files'), function (index, value) {
//        uploadFilesArray.push($("#uploadFile").prop('files')[index].name);
//    });

//    if (uploadFilesArray.length > 1) {
//        var ajaxData;
//        var stringedData;
//        ajaxData = {
//            "uploadFiles": uploadFilesArray
//        };

//        stringedData = JSON.stringify(ajaxData);

//        $.ajax({
//            type: "POST",
//            timeout: 50000,
//            url: '/Wager/UploadMultiple',
//            data: stringedData,
//            enctype: 'multipart/form-data',
//            success: function (data) {
//                alert('success');

//            }
//        });
//    } else {
//        ajaxData = {
//            "uploadFile": uploadFilesArray
//        };

//        stringedData = JSON.stringify(ajaxData);

//        $.ajax({
//            type: "POST",
//            timeout: 50000,
//            url: '/Wager/Upload',
//            data: stringedData,
//            success: function (data) {
//                alert('success');

//            }
//        });
//    }

    
//}

$(function () {
    //$('#fileUpload').fileupload({
    //    dataType: 'json',
    //    done: function (e, data) {
    //        $.each(data.result.files, function (index, file) {
    //            $('<p/>').text(file.name).appendTo(document.body);
    //        });
    //    }
    //});

    //$('#upload').click(UploadFiles);
});

//$("#fileUpload").prop('files')[index].name


//$(function () {
//    var upload = $('#upload');
//    var submit = $('#submit');

//    upload.click(UploadContests);
//    submit.click(SaveContests);

//})

//function CheckFileName() {
//    var fileName = $("#fileUpload").prop('files')[0].name;
//    //debugger;
//    if (fileName == "") {
//        alert("Browse to upload a valid File with xls / xlsx extension");
//        return false;
//    }
//    else if (fileName.split(".")[1].toUpperCase() == "XLS" || fileName.split(".")[1].toUpperCase() == "XLSX")
//        return true;
//    else {
//        alert("File with " + fileName.split(".")[1] + " is invalid. Upload a validfile with xls / xlsx extensions");
//        return false;
//    }
//    return true;
//}