function olustur() {
    //var marka = document.getElementById("tbMarka").value;
    var marka = $("#tbMarka").val();
    //var model = document.getElementById("tbModel").value;
    var model = $("#tbModel").val();
    var hiz = $("#tbHiz").val();
    ////var hafizaJs = document.getElementById("ddlHafiza").value;
    //console.log(hafizaJs);
    var hafiza = $("#ddlHafiza").val();
    //var suSogutmaJs = document.getElementById("cbSu").checked;
    //console.log(suSogutmaJs);
    var suSogutma = $("#cbSu").prop("checked");
    //console.log(suSogutma);
    var uretim = $("#dtpUretim").val();
    //var uretimJs = document.getElementById("dtpUretim").value;
    //console.log(uretimJs);

    let bilgisayar = new Bilgisayar(marka, model, hiz, hafiza, suSogutma, uretim)
    console.log(bilgisayar);

    /*document.getElementById("marka").innerText = bilgisayar.marka;*/
    $("#marka").text(bilgisayar.marka);
    $("#model").text(bilgisayar.model);
    $("#hiz").text(bilgisayar.hiz + " Ghz");
    $("#hafiza").text(bilgisayar.hafiza + " GB");

    var suSogutmaTernary = bilgisayar.suSogutma ? "Var" : "Yok";
    $("#susogutma").text(suSogutmaTernary);

    var tarih = new Date("2022-04-21T15:14:36");
    $("#uretim").text(tarih.toLocaleString('en-US'));
    $("#uretimtarihi").text(bilgisayar.uretim.toLocaleString("tr-TR"));

    alert(bilgisayar.markaVeModelGetir());


    //console.log(degisken1);//undefined
    //var degisken1 = 'D1';
    //console.log(degisken1); //D1

    //console.log(degisken2); //hata fırlatıyor
    //let degisken2 = 'D2';
    //console.log(degisken2); //hatadan dolayı yazdırılmaz
}
class Bilgisayar {
    constructor(marka, model, hiz, hafiza, suSogutma, uretim) {
        this.marka = marka;
        this.model = model;
        this.hiz = hiz;
        this.hafiza = hafiza;
        this.suSogutma = suSogutma;
        this.uretim = uretim;
    }
    markaVeModelGetir() {
        return this.marka + " " + this.model;
    }

}