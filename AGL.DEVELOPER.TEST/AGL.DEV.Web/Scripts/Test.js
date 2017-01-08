document.addEventListener("DOMContentLoaded", function (event) {

    var ddlLocation = document.getElementById('Location');
    var btnSave = document.getElementById('btnSave');

    btnSave.addEventListener('click', function ()
    {
        var seletedOption = ddlLocation.options[ddlLocation.selectedIndex];

        var data = JSON.stringify({ 
            locationId: parseInt(seletedOption.value, 10), 
            locationName: seletedOption.text 
        });

        var xmlDoc = getXmlDoc();

        xmlDoc.open('POST', '/Home/Test', true);

        xmlDoc.setRequestHeader("Content-type", "application/json; charset=utf-8");

        xmlDoc.onreadystatechange = function () {
            if (xmlDoc.readyState === 4 && xmlDoc.status === 200) {

                if (xmlDoc.response)
                {
                    var response = JSON.parse(xmlDoc.response);

                    if (response.Result)
                        alert(response.Result);
                }
            }
        }

        xmlDoc.send(data);
    },
    false);

    function getXmlDoc() {
        var xmlDoc;

        if (window.XMLHttpRequest) {
            // code for IE7+, Firefox, Chrome, Opera, Safari
            xmlDoc = new XMLHttpRequest();
        }
        else {
            // code for IE6, IE5
            xmlDoc = new ActiveXObject("Microsoft.XMLHTTP");
        }

        return xmlDoc;
    }
});