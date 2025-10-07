<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm10.aspx.cs" Inherits="petshopint2.WebForm10" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="tr">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>PetShop - Ürün Detayı</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <style>
        body {
            background: linear-gradient(135deg, #87CEEB 0%, #FFD700 50%, #FF69B4 100%);
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
            background: url('https://www.transparenttextures.com/patterns/paws.png') repeat, radial-gradient(circle, rgba(255,255,255,0.2), rgba(0,0,0,0.1));
            opacity: 0.08;
            z-index: -1;
            animation: backgroundShift 20s infinite linear;
        }
        @keyframes backgroundShift {
            0% { background-position: 0 0; }
            100% { background-position: 100% 100%; }
        }
        .navbar {
            background-color: rgba(255, 255, 255, 0.95);
            border-bottom: 3px solid #FF69B4;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.3);
        }
        .navbar-brand img {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            border: 2px solid #FFD700;
            transition: transform 0.3s ease;
        }
        .navbar-brand img:hover {
            transform: rotate(15deg) scale(1.2);
        }
        .nav-link {
            color: #FFD700 !important;
            font-weight: bold;
            transition: color 0.3s ease;
        }
        .nav-link:hover {
            color: #FF69B4 !important;
        }
        .container {
            margin-top: 80px;
            max-width: 800px;
            background-color: rgba(255, 255, 255, 0.92);
            padding: 40px;
            border-radius: 20px;
            box-shadow: 0 0 25px rgba(0, 0, 0, 0.25);
            position: relative;
            border: 3px solid #FFD700;
            text-align: center;
        }
        .container::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('https://www.transparenttextures.com/patterns/paws.png');
            opacity: 0.15;
            z-index: 0;
        }
        .container * {
            position: relative;
            z-index: 1;
        }
        h1 {
            color: #FF69B4;
            font-weight: bold;
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.15);
            margin-bottom: 20px;
        }
        .product-detail {
            background-color: #fff;
            border: 2px solid #FFD700;
            border-radius: 15px;
            padding: 20px;
            margin-bottom: 20px;
            transition: transform 0.3s ease;
        }
        .product-detail:hover {
            transform: scale(1.02);
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.35);
        }
        .product-detail img {
            max-width: 100%;
            height: 400px;
            object-fit: cover;
            border-radius: 10px;
            margin-bottom: 20px;
        }
        .product-detail h2 {
            color: #FF69B4;
            font-weight: bold;
            margin-bottom: 15px;
        }
        .product-detail p {
            color: #333;
            margin-bottom: 10px;
            font-size: 1.1em;
        }
        .product-detail .price {
            color: #FFD700;
            font-size: 1.5em;
            font-weight: bold;
            margin-bottom: 15px;
        }
        .product-detail .stock {
            color: #666;
            font-size: 1em;
        }
        .btn-sepet {
            background-color: #FF69B4;
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 10px;
            font-size: 1.1em;
            transition: background-color 0.3s ease;
            margin-top: 20px;
        }
        .btn-sepet:hover {
            background-color: #FFD700;
        }
        .footer {
            text-align: center;
            margin-top: 20px;
            font-size: 18px;
            color: #666;
            position: relative;
            bottom: 0;
            width: 100%;
        }
        .paw-decoration {
            position: absolute;
            font-size: 35px;
            opacity: 0.4;
            color: #FFD700;
            pointer-events: none;
        }
        .paw-1 { top: 10px; left: 20px; transform: rotate(20deg); }
        .paw-2 { bottom: 10px; right: 20px; transform: rotate(-30deg); }
        .paw-3 { top: 50%; left: -10px; transform: rotate(45deg); }
        .paw-4 { bottom: 50%; right: -10px; transform: rotate(-45deg); }
        .floating-paw {
            position: fixed;
            font-size: 40px;
            color: #FF69B4;
            opacity: 0.25;
            pointer-events: none;
            animation: float 8s infinite ease-in-out;
        }
        @keyframes float {
            0%, 100% { transform: translateY(0) rotate(0deg); }
            50% { transform: translateY(-30px) rotate(15deg); }
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
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg">
            <div class="container-fluid">
                <a class="navbar-brand" href="WebForm8.aspx">
                    <img src="foto/logo2.png" alt="PetShop Logo" />
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="WebForm7.aspx">İletişim</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="WebForm3.aspx">Kullanıcı Girişi</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="WebForm1.aspx">Yönetici Girişi</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="WebForm8.aspx">Ürünler</a>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkCikis" runat="server" CssClass="nav-link" OnClick="lnkCikis_Click" Text="Çıkış Yap" />
                        </li>
                        <li class="nav-item">
                            <asp:HyperLink ID="lnkSepet" runat="server" CssClass="nav-link" NavigateUrl="~/WebForm5.aspx" Text="Sepete Git" />
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Main Content -->
        <div class="container">
            <h1>Ürün Detayı</h1>
            <asp:Label ID="lblMesaj" runat="server" CssClass="text-danger d-block text-center mb-3"></asp:Label>
            <div class="product-detail">
                <asp:Image ID="imgUrun" runat="server" AlternateText="Ürün Resmi" />
                <h2><asp:Label ID="lblUrunAdi" runat="server" Text=""></asp:Label></h2>
                <p class="price"><asp:Label ID="lblFiyat" runat="server" Text=""></asp:Label></p>
                <p><asp:Label ID="lblAciklama" runat="server" Text=""></asp:Label></p>
                <p class="stock">Stok: <asp:Label ID="lblStok" runat="server" Text=""></asp:Label></p>
                <p>Kategori: <asp:Label ID="lblKategori" runat="server" Text=""></asp:Label></p>
                <p>Marka: <asp:Label ID="lblMarka" runat="server" Text=""></asp:Label></p>
                <p>Ürün Türü: <asp:Label ID="lblUrunTuru" runat="server" Text=""></asp:Label></p>
                <p>Renk: <asp:DropDownList ID="ddlRenk" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlRenk_SelectedIndexChanged"></asp:DropDownList></p>
                <asp:Button ID="btnSepeteEkle" runat="server" Text="Sepete Ekle" CssClass="btn-sepet" OnClick="btnSepeteEkle_Click" />
            </div>

            <!-- Footer -->
            <div class="footer">
                🐾 🐾 🐾 🐾 🐶 🐱
            </div>

            <!-- Paw Decorations -->
            <span class="paw-decoration paw-1">🐾</span>
            <span class="paw-decoration paw-2">🐾</span>
            <span class="paw-decoration paw-3">🐾</span>
            <span class="paw-decoration paw-4">🐾</span>
        </div>

        <!-- Cat/Dog Animation -->
        <span class="dog">🐶</span>
        <span class="cat">🐱</span>
    </form>
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

        window.onload = function () {
            createFloatingPaws();
            triggerAnimation();
        };
    </script>
</body>
</html>