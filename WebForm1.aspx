<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="vetcare.WebForm1" %>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>PetShop Otomasyonu - Yönetici Girişi</title>
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
        .navbar {
            background-color: #FFA500;
            border-bottom: 3px solid #32CD32;
        }
        .navbar .nav-link, .navbar-brand {
            color: white !important;
            font-weight: bold;
            transition: color 0.3s ease;
        }
        .navbar .nav-link:hover {
            color: #32CD32 !important;
        }
        .container {
            margin-top: 80px;
            max-width: 500px;
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
            display: flex;
            align-items: center;
            justify-content: center;
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
        h3 {
            color: #FFA500;
            text-align: center;
            font-weight: bold;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.1);
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
        <nav class="navbar navbar-expand-lg">
            <div class="container-fluid">
                <a class="navbar-brand" href="WebForm1.aspx">
                    <img src="foto/logo2.png" alt="PetShop Logo" style="width: 40px; height: 40px; margin-right: 10px; vertical-align: middle; border-radius: 50%; object-fit: cover;" />
                    PetShop Otomasyonu
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                            <asp:LinkButton ID="btnAnasayfa" runat="server" CssClass="nav-link" OnClick="btnAnasayfa_Click">Anasayfa</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="container">
            <img src="foto/logo2.png" alt="PetShop Logo" class="logo" />
            <h3 class="mb-4">PetShop Yönetici Girişi</h3>
            <div class="mb-3">
                <label for="txtKullaniciAdi" class="form-label">Yönetici Kullanıcı Adı:</label>
                <asp:TextBox ID="txtKullaniciAdi" runat="server" CssClass="form-control" placeholder="Kullanıcı adınızı girin" required="required"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtSifre" class="form-label">Şifre:</label>
                <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifrenizi girin" required="required"></asp:TextBox>
            </div>
            <div class="d-grid">
                <asp:Button ID="btnAdminGiris" runat="server" Text="Yönetici Girişi" CssClass="btn btn-custom-orange" OnClick="btnAdminGiris_Click" />
            </div>
            <div class="mt-3">
                <asp:Label ID="lblMesaj" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <span class="paw-decoration paw-1">🐾</span>
            <span class="paw-decoration paw-2">🐾</span>
            <span class="paw-decoration paw-3">🐾</span>
            <span class="paw-decoration paw-4">🐾</span>
        </div>

        <span class="dog">🐶</span>
        <span class="cat">🐱</span>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
        <script>
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

            document.getElementById('btnAdminGiris').addEventListener('click', function () {
                // Animasyon, sunucu tarafında tetiklenecek
            });

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
        </script>
    </form>
</body>
</html>