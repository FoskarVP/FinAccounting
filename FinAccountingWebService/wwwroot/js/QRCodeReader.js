document.addEventListener('DOMContentLoaded', function () {

    $('#answer').html(Html5Qrcode.getCameras());

    const html5QrCode = new Html5Qrcode("reader");
    const fileinput = document.getElementById('qr-input-file');
    fileinput.addEventListener('change', e => {
        if (e.target.files.length == 0) {
            // No file selected, ignore 
            return;
        }

        const imageFile = e.target.files[0];
        // Scan QR Code
        html5QrCode.scanFile(imageFile, true)
            .then(decodedText => {
                // success, use decodedText
                console.log(decodedText);
                $('#answer').html(decodedText);
            })
            .catch(err => {
                // failure, handle it.
                console.log(`Error scanning file. Reason: ${err}`)
                $('#answer').html(err);
            });
    });

    function onScanSuccess(decodedText, decodedResult) {
        console.log("Code matched = ${decodedText}", decodedResult);
        console.log(decodedText);

        $("#answer").html("Code matched : " + decodedText);

        html5QrCode.stop().then((ignore) => {
            console.log("QR Code scanning stopped.");
        }).catch((err) => {
            console.log("Unable to stop scanning.");
        });

        receiptByQRRaw(decodedText);

        $('#answer').html(decodedText + "    11111111111111111111111111111111111111");
    }

    function onScanFailure(error) {
        console.warn(`Code scan error = ${error}`);
    }

    //easy mode
    const config = { fps: 10, qrbox: { width: 400, height: 400 } };
    //let html5QrcodeScanner = new Html5QrcodeScanner("reader", config, /* verbose= */ false);
    //html5QrcodeScanner.render(onScanSuccess, onScanFailure);


    function scan() {
        $("#answer").html("scanning...")
        html5QrCode.start({ facingMode: { exact: "environment" } }, config, onScanSuccess);
    }

    document.getElementById("qr-scan").addEventListener("click", scan, false);

}, false);