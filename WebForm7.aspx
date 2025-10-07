<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm7.aspx.cs" Inherits="petshopint2.WebForm7" %>
<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>PetShop - İletişim</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <style>
        body {
            background: linear-gradient(135deg, #32CD32, #FFA500);
            font-family: 'Comic Sans MS', 'Arial', sans-serif;
            color: #333;
            overflow-x: hidden;
            position: relative;
            min-height: 100vh;
        }
        body::before {
            content: '';
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('https://www.transparenttextures.com/patterns/paws.png') repeat;
            opacity: 0.05;
            z-index: -1;
        }
        .container {
            margin-top: 80px;
            max-width: 600px;
            background-color: rgba(255, 255, 255, 0.9);
            padding: 40px;
            border-radius: 20px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
            position: relative;
            overflow: hidden;
            border: 3px solid #32CD32;
        }
        .container::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('https://www.transparenttextures.com/patterns/paws.png');
            opacity: 0.1;
            z-index: 0;
        }
        .container * {
            position: relative;
            z-index: 1;
        }
        .logo {
            display: block;
            margin: 0 auto 20px;
            width: 100px;
            height: 100px;
            border-radius: 50%;
            border: 4px solid #32CD32;
            transition: transform 0.3s ease;
            object-fit: cover;
        }
        .logo:hover {
            transform: rotate(10deg) scale(1.1);
        }
        .btn-custom-orange {
            background-color: #FFA500;
            color: white;
            border: none;
            border-radius: 25px;
            padding: 12px 20px;
            font-weight: bold;
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            text-decoration: none;
        }
        .btn-custom-orange:hover {
            background-color: #32CD32;
            color: white;
            transform: scale(1.05);
        }
        .btn-custom-orange::before {
            content: '🐾';
            position: absolute;
            left: 15px;
            font-size: 20px;
            opacity: 0.7;
        }
        .form-label {
            color: #32CD32;
            font-weight: bold;
        }
        .form-control {
            border: 2px solid #32CD32;
            border-radius: 10px;
            transition: all 0.3s ease;
        }
        .form-control:focus {
            border-color: #FFA500;
            box-shadow: 0 0 10px rgba(255, 165, 0, 0.5);
            outline: none;
        }
        .text-danger {
            font-size: 14px;
            color: #FF4500;
            text-align: center;
            display: block;
        }
        .text-success {
            font-size: 14px;
            color: #32CD32;
            text-align: center;
            display: block;
        }
        h3 {
            color: #FFA500;
            text-align: center;
            font-weight: bold;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.1);
        }
        .footer {
            text-align: center;
            margin-top: 20px;
            font-size: 18px;
            color: #666;
        }
        .paw-decoration {
            position: absolute;
            font-size: 35px;
            opacity: 0.3;
            color: #32CD32;
            pointer-events: none;
        }
        .paw-1 { top: 10px; left: 20px; transform: rotate(20deg); }
        .paw-2 { bottom: 10px; right: 20px; transform: rotate(-30deg); }
        .paw-3 { top: 50%; left: -10px; transform: rotate(45deg); }
        .paw-4 { bottom: 50%; right: -10px; transform: rotate(-45deg); }
        .floating-paw {
            position: fixed;
            font-size: 40px;
            color: #FFA500;
            opacity: 0.2;
            pointer-events: none;
            animation: float 10s infinite ease-in-out;
        }
        @keyframes float {
            0%, 100% { transform: translateY(0) rotate(0deg); }
            50% { transform: translateY(-20px) rotate(10deg); }
        }
        .cat, .dog {
            position: fixed;
            font-size: 50px;
            z-index: 10;
            display: none;
        }
        .cat {
            bottom: 20px;
            left: -60px;
            animation: chaseCat 5s linear forwards;
        }
        .dog {
            bottom: 20px;
            left: -120px;
            animation: chaseDog 5s linear forwards;
        }
        @keyframes chaseCat {
            0% { left: -60px; transform: rotate(0deg); }
            100% { left: 100vw; transform: rotate(360deg); }
        }
        @keyframes chaseDog {
            0% { left: -120px; transform: rotate(0deg); }
            100% { left: calc(100vw - 60px); transform: rotate(-360deg); }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <img src="foto/logo2.png" alt="PetShop Logo" class="logo" />
            <h3 class="mb-4">PetShop - İletişim</h3>
            <a href="WebForm8.aspx" class="btn-custom-orange mb-4">Anasayfa</a>

            <!-- İletişim Formu -->
            <div class="iletisim-formu mt-4">
                <div class="mb-3">
                    <label for="txtAdSoyad" class="form-label">Ad Soyad:</label>
                    <asp:TextBox ID="txtAdSoyad" runat="server" CssClass="form-control" placeholder="Adınızı ve soyadınızı girin"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtEposta" class="form-label">E-posta:</label>
                    <asp:TextBox ID="txtEposta" runat="server" CssClass="form-control" TextMode="Email" placeholder="E-posta adresinizi girin"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtKonu" class="form-label">Konu:</label>
                    <asp:TextBox ID="txtKonu" runat="server" CssClass="form-control" placeholder="Mesaj konunuzu girin"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="txtMesaj" class="form-label">Mesaj:</label>
                    <asp:TextBox ID="txtMesaj" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Mesajınızı yazın"></asp:TextBox>
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnGonder" runat="server" Text="Mesaj Gönder" CssClass="btn btn-custom-orange" OnClientClick="return validateForm();" OnClick="btnGonder_Click" />
                </div>
                <div class="mt-3">
                    <asp:Label ID="lblMesaj" runat="server" CssClass="text-danger"></asp:Label>
                </div>
            </div>

            <!-- Footer -->
            <div class="footer mt-4">
                🐾 🐾 🐾 🐾 🐶 🐱
            </div>

            <span class="paw-decoration paw-1">🐾</span>
            <span class="paw-decoration paw-2">🐾</span>
            <span class="paw-decoration paw-3">🐾</span>
            <span class="paw-decoration paw-4">🐾</span>
        </div>

        <span class="dog">🐶</span>
        <span class="cat">🐱</span>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <script>
        function validateForm() {
            var adSoyad = document.getElementById('<%= txtAdSoyad.ClientID %>').value.trim();
            var eposta = document.getElementById('<%= txtEposta.ClientID %>').value.trim();
            var konu = document.getElementById('<%= txtKonu.ClientID %>').value.trim();
            var mesaj = document.getElementById('<%= txtMesaj.ClientID %>').value.trim();
            var lblMesaj = document.getElementById('<%= lblMesaj.ClientID %>');

            lblMesaj.innerText = "";
            lblMesaj.className = "text-danger";

            if (adSoyad === "" || eposta === "" || konu === "" || mesaj === "") {
                lblMesaj.innerText = "Lütfen tüm alanları doldurun!";
                return false;
            }
            if (!/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(eposta)) {
                lblMesaj.innerText = "Geçerli bir e-posta adresi girin!";
                return false;
            }
            return true;
        }

        function triggerAnimation() {
            const cat = document.querySelector('.cat');
            const dog = document.querySelector('.dog');
            cat.style.display = 'block';
            dog.style.display = 'block';
            setTimeout(() => {
                cat.style.display = 'none';
                dog.style.display = 'none';
                cat.style.left = '-60px';
                dog.style.left = '-120px';
            }, 5000);
        }

        function createFloatingPaws() {
            const pawCount = 20;
            for (let i = 0; i < pawCount; i++) {
                const paw = document.createElement('span');
                paw.className = 'floating-paw';
                paw.textContent = '🐾';
                paw.style.left = Math.random() * 100 + 'vw';
                paw.style.top = Math.random() * 100 + 'vh';
                paw.style.animationDelay = Math.random() * 5 + 's';
                paw.style.transform = `rotate(${Math.random() * 360}deg)`;
                document.body.appendChild(paw);
            }
        }

        window.onload = createFloatingPaws;

        // Trigger animation after successful server-side submission
        function onSubmissionSuccess() {
            var lblMesaj = document.getElementById('<%= lblMesaj.ClientID %>');
            lblMesaj.className = "text-success";
            lblMesaj.innerText = "Mesajınız başarıyla gönderildi!";
            triggerAnimation();
        }
    </script>
</body>
</html>