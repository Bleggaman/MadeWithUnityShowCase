function MakeRequest() {
    document.write("Bleg");
    setTimeout(ChangePage, 1000);
}

function ChangePage() {
    console.log("Bleg 1");
    
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            console.log("Bleg 2");
            document.getElementById("Bleg").innerHTML = "<h2>" + this.responseText + "</h2>";
       }
    };
    xhttp.open("GET", "RequestProject.cshtml", true);
    xhttp.send();
    console.log("Bleg 3");
}
