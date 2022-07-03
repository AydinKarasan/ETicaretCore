$(document).ready(function () {
    $("#bPalindrom").click(function () {
        console.log(1);
        var kelime = $("#tbKelime").val();
        if (palindromMu(kelime)) {
            $("#sPalindromText").text("Kelime palindromdur");
            $("#sPalindromHtml").html('<span style="color:blue;">Kelime palindromdur.</span>');
            $("#tbPalindromValue").val("kelime palindromdur.");
        } else {
            $("#sPalindromText").text("Kelime palindrom değildir");
            $("#sPalindromHtml").html('<span style="color:red;">Kelime palindrom değildir.</span>');
            $("#tbPalindromValue").val("kelime palindrom değildir.");
        }
    });
    
});

function palindromMu(kelime) {
    var tersKelime = "";
    var i = kelime.length - 1;
    while (i >= 0) {
        tersKelime += kelime[i];
        i--;
    }
    if (kelime == tersKelime)
        return true;
    //else
    return false;
}
