function MakeRequest() {
    document.write("Bleg");
    setTimeout(ChangePage, 1000);
}

function ChangePage() {
    console.log("Bleg 1");
    
    var xhttp = new XMLHttpRequest();
    xhttp.open("GET", "RequestPage.aspx", true);
    xhttp.send();
    console.log("Bleg 2");
}
