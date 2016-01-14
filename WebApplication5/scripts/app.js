var url = 'http://localhost:53676/api/ProdMfg/';

function find() {
    var id = $('#mfgIdFind').val();
    $.getJSON(url + "/" + id,
        function (data) {
            if (data == null) {
                $('#manufacturerFind').text('Manufacturer not found.');
            }
            var str = data.mfg + ': ' + data.mfgDiscount + '%';
            $('#manufacturerFind').text(str);
        })
    .fail(
        function (jqueryHeaderRequest, textStatus, err) {
            $('#manufacturerFind').text('Find error: ' + err);
        });
}




    // Creates and sends the following JSON:
    // {"manufacturers":
    // [{"mfg":"Caterpillar","mfgDiscount":20},{"mfg":"Honda","mfgDiscount":10}]}
    function sendData() {
        var mfgListVM = new Object();
        var manufacturer1 = new Object();
        manufacturer1.mfg = "Caterpillar";
        manufacturer1.mfgDiscount = 20;

        var manufacturer2 = new Object();
        manufacturer2.mfg = "Honda";
        manufacturer2.mfgDiscount = 10;

        var manufacturers = [];
        manufacturers.push(manufacturer1);
        manufacturers.push(manufacturer2);

        mfgListVM.manufacturers = manufacturers;
        var jsonString = JSON.stringify(mfgListVM);

        $.ajax({
            url: url,
            type: 'POST',
            data: jsonString,
            contentType: "application/json;charset=utf-8",
        }).done(function (data) {
            callback(data);
        }).error(function (jqXHR, textStatus, errorThrown) {
            $('#value1').text(jqXHR.responseText || textStatus);
        });
    }

    // Receives and parses the following JSON:
    // {"message": "The data was posted successfully!",
    //  "manufacturers": [ {  "name": "Caterpillar" }, { "name": "Honda" }]}
    function callback(data) {
        var str = data.message;
        $('#value2').text(str);
        data.manufacturers.forEach(function (val) {
            // Find element with id 'value1' and add <br/> to it.
            var valTag = document.getElementById("value2");
            var br = document.createElement("br");
            valTag.appendChild(br);

            // Append product data to 'value1' element.
            var prodStr = "Manufacturer name: " + val.name;
            var productSpan = document.createElement('span')
            productSpan.innerHTML = prodStr;
            valTag.appendChild(productSpan);
        });
    }




