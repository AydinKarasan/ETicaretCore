function topla() {
    var sayi1 = document.getElementById('sayi1');
    var sayi1value = parseInt(sayi1.value); // parsefloat() aynı
    var sayi2 = parseInt(document.getElementById('sayi2').value); // üstteki gibi de yapılabilirdi obje döndüğü için direkt . value diyerek değeri aldık
    var toplam = sayi1value + sayi2;
    var sonuc = document.getElementById('sonuc');
    sonuc.innerText = toplam; //7
    sonuc.innerHTML = "<b>" + toplam + "</b>"; // <b>7</b>
}
//function cikar() {
//    var sayi1 = parseInt(document.getElementById("sayi1").value);
//    var sayi2 = parseInt(document.getElementById("sayi2").value);
//    var cikar = sayi1 - sayi2;
//    var sonuc1 = document.getElementById("sonuc1");
//    sonuc1.innerHTML = "<b>" + cikar + "<b>";
//}
//function carp() {
//    var sayi1 = parseInt(document.getElementById("sayi1").value);
//    var sayi2 = parseInt(document.getElementById("sayi2").value);
//    var carp = sayi1 * sayi2;
//    var sonuc2 = document.getElementById("sonuc2");
//    sonuc2.innerHTML = "<b>" + carp + "<b>";
//}
//function bol() {
//    var sayi1 = parseInt(document.getElementById("sayi1").value);
//    var sayi2 = parseInt(document.getElementById("sayi2").value);
//    var bol = sayi1 / sayi2;
//    var sonuc3 = document.getElementById("sonuc3");
//    sonuc3.innerHTML = "<b>" + bol + "<b>";
//}
var test = true;//global değişken // burasını true olarak programı çalıştırdık,hatamızı düzelttikten sonra program çalıştı false yaptık

function faktoriyel() {
    var sayi1 = parseInt(document.getElementById("sayi1").value);
    //var dizi = new Array();
    var dizi = [];
    var diziIndex = 0;
    for (var i = sayi1; i > 2; i--) {
        dizi[diziIndex] = i;
        diziIndex++;
    }
    diziIndex = 0;
    var faktoriyel = 2;
    while (diziIndex < dizi.length) {
        if (test) {
            console.log(dizi[diziIndex]);
        }
        faktoriyel *= dizi[diziIndex];
        //faktoriyel = faktoriyel * dizi[diziIndex];
        diziIndex++;
    }
    var sonuc = document.getElementsByTagName("span")[0];
    sonuc.innerText = faktoriyel;
    if (faktoriyel >= 500)
        sonuc.style.color= "red";
    else
    sonuc.style.color = "green";
}
function rengiDegistir(renk) {
    var body = document.getElementsByTagName("body")[0];
    body.style.backgroundColor = renk;

}