<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="petshopint2.WebForm5" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="tr">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>PetShop - Sepet</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <style type="text/css">
        body {
            font-family: 'Comic Sans MS', 'Arial', sans-serif;
            background: linear-gradient(135deg, #32CD32, #FFA500);
            color: #333;
            margin: 0;
            padding: 0;
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
            width: 90%;
            margin: 80px auto 20px;
            padding: 20px;
            display: flex;
            flex-wrap: wrap;
            background-color: rgba(255, 255, 255, 0.9);
            border-radius: 20px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.2);
            border: 3px solid #32CD32;
            position: relative;
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
        .cart-section {
            flex: 2;
            margin-right: 20px;
        }
        .detail-section {
            flex: 1;
            background-color: white;
            padding: 15px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            border: 2px solid #32CD32;
        }
        .header {
            background-color: #FFA500;
            color: white;
            padding: 15px;
            text-align: center;
            border-radius: 5px;
            margin-bottom: 20px;
            position: relative;
            border: 2px solid #32CD32;
        }
        .header::before, .header::after {
            content: "🐾";
            font-size: 24px;
            position: absolute;
            top: 15px;
        }
        .header::before {
            left: 20px;
        }
        .header::after {
            right: 20px;
        }
        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        .gridview th, .gridview td {
            border: 1px solid #32CD32;
            padding: 8px;
            text-align: center;
        }
        .gridview th {
            background-color: #FFA500;
            color: white;
        }
        .gridview tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .gridview img {
            width: 50px;
            height: 50px;
            cursor: pointer;
        }
        .action-button {
            border: none;
            padding: 5px 10px;
            border-radius: 25px;
            cursor: pointer;
            margin: 2px;
            font-weight: bold;
            transition: all 0.3s ease;
        }
        .quantity-button {
            width: 30px;
            height: 30px;
            border-radius: 50%;
            font-size: 16px;
            line-height: 1;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            margin: 0 5px;
            cursor: pointer;
            transition: all 0.3s ease;
        }
        .plus-button {
            background-color: #4CAF50;
            color: white;
        }
        .plus-button:hover {
            background-color: #FF69B4;
            transform: scale(1.1);
        }
        .minus-button {
            background-color: #dc3545;
            color: white;
        }
        .minus-button:hover {
            background-color: #FFD700;
            transform: scale(1.1);
        }
        .ekle-buton {
            background-color: #4CAF50;
            color: white;
        }
        .ekle-buton:hover {
            background-color: #32CD32;
            transform: scale(1.05);
        }
        .onayla-buton {
            background-color: #FFA500;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 25px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            transition: all 0.3s ease;
        }
        .onayla-buton:hover {
            background-color: #32CD32;
            transform: scale(1.05);
        }
        .cart-summary {
            margin-top: 20px;
            padding: 15px;
            background-color: #fff;
            border-radius: 5px;
            border: 1px solid #32CD32;
            text-align: right;
        }
        .message-label {
            color: #FF69B4;
            font-weight: bold;
            margin-top: 10px;
            margin-bottom: 10px;
            display: block;
        }
        .modal-content {
            border: 3px solid #32CD32;
            border-radius: 15px;
        }
        .modal-header, .modal-footer {
            background-color: #FFA500;
            color: white;
        }
        .modal-body {
            background-color: rgba(255, 255, 255, 0.9);
        }
        .modal-title {
            font-weight: bold;
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
    <script type="text/javascript">
        function showProductDetails(imageUrl, description) {
            document.getElementById("modalImage").src = imageUrl;
            document.getElementById("modalDescription").innerText = description;
            var modal = new bootstrap.Modal(document.getElementById("productModal"));
            modal.show();
        }

        function showPurchaseConfirmation() {
            var modal = new bootstrap.Modal(document.getElementById("purchaseModal"));
            modal.show();
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

        window.onload = function () {
            createFloatingPaws();
            triggerAnimation();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg">
            <div class="container-fluid">
                <a class="navbar-brand" href="WebForm8.aspx">
                    <img src="foto/logo2.png" alt="PetShop Logo" />
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Close">
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
                            <asp:HyperLink ID="lnkSepet" runat="server" CssClass="nav-link" NavigateUrl="~/WebForm5.aspx" Text="Sepetim" />
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Main Content -->
        <div class="container">
            <!-- Cart Section (Left) -->
            <div class="cart-section">
                <div class="header">
                    <h2>PetShop - Sepetiniz</h2>
                    <asp:Label ID="lblKullaniciAdi" runat="server" Text="Hoş Geldiniz!" />
                    <asp:Label ID="lblMesaj" runat="server" CssClass="message-label" Visible="false" />
                </div>

                <!-- Cart GridView -->
                <asp:GridView ID="gvSepet" runat="server" CssClass="gridview" AutoGenerateColumns="False" OnRowCommand="gvSepet_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="UrunID" HeaderText="Ürün ID" Visible="false" />
                        <asp:BoundField DataField="UrunAdi" HeaderText="Ürün Adı" />
                        <asp:BoundField DataField="Fiyat" HeaderText="Fiyat" DataFormatString="{0:C}" />
                        <asp:TemplateField HeaderText="Resim">
                            <ItemTemplate>
                                <asp:Image ID="imgUrun" runat="server" ImageUrl='<%# Eval("ResimYolu") %>' CssClass="img-fluid" Width="50px" Height="50px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Adet" HeaderText="Adet" />
                        <asp:BoundField DataField="ToplamFiyat" HeaderText="Toplam Fiyat" DataFormatString="{0:C}" />
                        <asp:TemplateField HeaderText="İşlemler">
                            <ItemTemplate>
                                <asp:Button ID="btnEkle" runat="server" Text="+" CssClass="quantity-button plus-button" CommandName="Ekle" CommandArgument='<%# Eval("UrunID") %>' />
                                <asp:Button ID="btnKaldir" runat="server" Text="-" CssClass="quantity-button minus-button" CommandName="Kaldir" CommandArgument='<%# Eval("UrunID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <!-- Past Orders GridView -->
                <h3>Geçmiş Siparişler</h3>
                <asp:Label ID="lblNoOrders" runat="server" Text="Geçmiş siparişiniz bulunmamaktadır." Visible="false" CssClass="message-label" />
                <asp:GridView ID="gvPastOrders" runat="server" CssClass="gridview" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvPastOrders_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="SiparisID" HeaderText="Sipariş ID" />
                        <asp:BoundField DataField="UrunAdi" HeaderText="Ürün Adı" />
                        <asp:BoundField DataField="Adet" HeaderText="Adet" />
                        <asp:BoundField DataField="ToplamFiyat" HeaderText="Toplam Fiyat" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="SiparisTarihi" HeaderText="Sipariş Tarihi" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                        <asp:BoundField DataField="SiparisNo" HeaderText="Sipariş No" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblModalDebug" runat="server" CssClass="message-label" Visible="false" />

                <!-- Summary -->
                <div class="cart-summary">
                    <asp:Label ID="lblToplamFiyat" runat="server" Text="Toplam: 0,00 TL" Font-Bold="true" />
                    <br />
                    <asp:Button ID="btnSepetiOnayla" runat="server" Text="Sepeti Onayla" CssClass="onayla-buton mt-2" OnClick="btnSepetiOnayla_Click" />
                </div>
            </div>

            <!-- Detail Section (Right) -->
            <div class="detail-section">
                <h4>Ürün Detayı</h4>
                <asp:Image ID="imgSelectedProduct" runat="server" ImageUrl="https://via.placeholder.com/150" Visible="false" CssClass="img-fluid mb-2" AlternateText="Ürün Resmi" />
                <asp:Label ID="lblProductDetail" runat="server" Text="Bir ürün seçin." />
                <asp:Button ID="btnAddToCartDetail" runat="server" Text="Sepete Ekle" CssClass="action-button ekle-buton mt-2" OnClick="btnAddToCartDetail_Click" Visible="false" />
            </div>

            <!-- Product Detail Modal -->
            <div class="modal fade" id="productModal" tabindex="-1" aria-labelledby="productModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="productModalLabel">Ürün Detayı</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <img id="modalImage" src="" alt="Ürün Resmi" class="img-fluid" />
                            <p id="modalDescription" class="mt-3"></p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Purchase Confirmation Modal -->
            <div class="modal fade" id="purchaseModal" tabindex="-1" aria-labelledby="purchaseModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="purchaseModalLabel">Sipariş Onayı</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblPurchaseMessage" runat="server" Text="Sipariş Onaylandı! 🐾" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Kapat</button>
                            <asp:Button ID="btnContinueShopping" runat="server" Text="Alışverişe Devam Et" CssClass="btn btn-primary" OnClick="btnContinueShopping_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Paw Decorations -->
            <span class="paw-decoration paw-1">🐾</span>
            <span class="paw-decoration paw-2">🐾</span>
            <span class="paw-decoration paw-3">🐾</span>
            <span class="paw-decoration paw-4">🐾</span>

            <!-- Cat/Dog Animation -->
            <span class="dog">🐶</span>
            <span class="cat">🐱</span>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>