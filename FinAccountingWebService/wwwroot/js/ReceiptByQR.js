

async function receiptByRequesits(storage, doc, attribute, date, total, time, type) {

    const response = await fetch("api/receipt/receiptByRequisits", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            fiscalStorage: storage,
            fiscalDocument: doc,
            fiscalAttribute: attribute,
            dateTime: date + 'T' + time,
            total: total,
            receiptType: type
        })
    });
    if (response.ok) {
        const receipt = await response.json();
        console.log(receipt);
        $("#answer").html(JSON.stringify(receipt))
        //reset();
        //document.querySelector("tbody").append(row(user));
    }
    else {
        const error = await response.json();
        console.log(error.message);
        alert(error.message);
    }
}

async function receiptByQRRaw(qrraw) {
    $("#answer").html("start sending by QRRaw")

    const response = await fetch("api/receipt/receiptByQRRaw", {
        method: "POST",
        headers: { "Accept": "text/plain", "Content-Type": "text/plain" },
        body: qrraw
    });
    if (response.ok) {
        const receipt = await response.json();
        console.log(receipt);
        $("#answer").html(JSON.stringify(receipt))
        //reset();
        //document.querySelector("tbody").append(row(user));
    }
    else {
        const error = await response.json();
        console.log(error.message);
        alert(error.message);
    }
}

async function receiptByQRUrl(qrUrl) {

    const response = await fetch("api/receipt/receiptByQRUrl", {
        method: "POST",
        headers: { "Accept": "text/plain", "Content-Type": "text/plain" },
        body: qrUrl
    });
    if (response.ok) {
        const receipt = await response.json();
        console.log(receipt);
        $("#answer").html(JSON.stringify(receipt))
    }
    else {
        const error = await response.json();
        console.log(error.message);
        alert(error.message);
    }
}

document.addEventListener('DOMContentLoaded', function () {

    // отправка формы
    document.forms["receiptRequisits"].addEventListener("submit", e => {
        e.preventDefault();
        const form = document.forms["receiptRequisits"];
        const storage = form.elements["fiscalStorage"].value;
        const doc = form.elements["fiscalDocument"].value;
        const attribute = form.elements["fiscalAttribute"].value;
        const total = form.elements["total"].value;
        const date = form.elements["date"].value;
        const time = form.elements["time"].value;
        const type = form.elements["receiptType"].value;

        receiptByRequesits(storage, doc, attribute, date, total, time, type);
    });

    document.forms["qrRaw"].addEventListener("submit", e => {
        e.preventDefault();
        const raw = document.forms["qrRaw"].elements["raw"].value;

        receiptByQRRaw(raw);
    });

    document.forms["qrUrl"].addEventListener("submit", e => {
        e.preventDefault();
        const raw = document.forms["qrUrl"].elements["url"].value;

        receiptByQRUrl(raw);
    });


}, false);