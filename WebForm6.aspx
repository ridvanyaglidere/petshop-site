<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm6.aspx.cs" Inherits="petshopint2.WebForm6" %>
<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>PetShop Otomasyonu - Admin Paneli</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js"></script>
    <style>
        body {
            background: linear-gradient(135deg, #32CD32, #FFA500);
            font-family: Arial, sans-serif;
            color: #333;
            overflow-x: hidden;
            position: relative;
            min-height: 100vh;
            margin: 0;
        }
        body::before {
            content: '';
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('https://www.transparenttextures.com/patterns/paws.png') repeat;
            opacity: 0.1;
            z-index: -1;
        }
        .container {
            display: flex;
            min-height: 100vh;
            padding: 0;
        }
        .sidebar {
            width: 200px;
            background: linear-gradient(to bottom, #FFA500, #FF8C00);
            padding: 20px 10px;
            position: fixed;
            height: 100%;
            overflow-y: auto;
            box-shadow: 3px 0 15px rgba(0, 0, 0, 0.2);
            transition: width 0.3s ease, transform 0.3s ease;
        }
        .sidebar-left {
            left: 0;
            border-right: 5px solid #32CD32;
            top: 0px;
        }
        .sidebar h2 {
            color: white;
            text-align: center;
            margin-bottom: 30px;
            font-size: 22px;
            font-weight: 700;
            text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.3);
        }
        .sidebar a {
            display: flex;
            align-items: center;
            color: white;
            padding: 12px 15px;
            text-decoration: none;
            margin-bottom: 8px;
            border-radius: 8px;
            font-size: 15px;
            font-weight: 500;
            transition: background-color 0.3s ease, padding 0.3s ease, transform 0.2s ease;
            position: relative;
            overflow: hidden;
        }
        .sidebar-left a {
            padding-left: 15px;
        }
        .sidebar a::before {
            content: '🐾';
            font-size: 18px;
            opacity: 0.9;
            transition: transform 0.3s ease;
            margin-right: 10px;
        }
        .sidebar a:hover {
            background-color: #32CD32;
            transform: scale(1.02);
        }
        .sidebar-left a:hover {
            padding-left: 20px;
        }
        .sidebar a:hover::before {
            transform: rotate(15deg);
        }
        .sidebar a.active {
            background-color: #32CD32;
            font-weight: 700;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        }
        .main-content {
            margin-left: 200px;
            padding: 30px;
            flex-grow: 1;
            background-color: rgba(255, 255, 255, 0.97);
            border-radius: 20px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
            margin: 20px;
        }
        .main-content h1 {
            color: #FFA500;
            text-align: center;
            font-weight: 700;
            font-size: 30px;
            text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.1);
            margin-bottom: 25px;
        }
        .main-content h2 {
            color: #32CD32;
            margin-top: 35px;
            margin-bottom: 15px;
            font-size: 24px;
            font-weight: 600;
            border-bottom: 2px solid #FFA500;
            padding-bottom: 5px;
        }
        .stats {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
            gap: 15px;
            margin-bottom: 30px;
        }
        .stat-box {
            background: linear-gradient(to bottom, #FFA500, #FF8C00);
            color: white;
            padding: 15px;
            border-radius: 10px;
            text-align: center;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            font-size: 15px;
            font-weight: 600;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }
        .stat-box:hover {
            transform: scale(1.05);
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
        }
        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
            font-size: 14px;
            background-color: #fff;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.05);
        }
        .gridview th, .gridview td {
            padding: 12px;
            border: 1px solid #32CD32;
            text-align: left;
        }
        .gridview th {
            background: linear-gradient(to bottom, #FFA500, #FF8C00);
            color: white;
            font-weight: 700;
            text-transform: uppercase;
        }
        .gridview tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .gridview tr:hover {
            background-color: #f0f0f0;
        }
        .command-button {
            background-color: #FF4500;
            color: white;
            border: none;
            padding: 6px 12px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 13px;
            font-weight: 500;
            transition: background-color 0.3s ease, transform 0.2s ease;
            margin-right: 5px;
        }
        .command-button:hover {
            background-color: #e03e00;
            transform: scale(1.05);
        }
        .read-button {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 6px 12px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 13px;
            font-weight: 500;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }
        .read-button:hover {
            background-color: #218838;
            transform: scale(1.05);
        }
        .unread-button {
            background-color: #dc3545;
            color: white;
            border: none;
            padding: 6px 12px;
            border-radius: 5px;
            cursor: pointer;
            font-size: 13px;
            font-weight: 500;
            transition: background-color 0.3s ease, transform 0.2s ease;
        }
        .unread-button:hover {
            background-color: #c82333;
            transform: scale(1.05);
        }
        .form-group {
            margin-bottom: 20px;
            max-width: 550px;
            position: relative;
            display: flex;
            flex-wrap: wrap;
            align-items: center;
            gap: 15px;
        }
        .form-group label {
            color: #32CD32;
            font-weight: 600;
            font-size: 15px;
            margin-bottom: 5px;
            flex: 0 0 120px;
            text-align: right;
        }
        .form-group input[type="text"],
        .form-group textarea,
        .form-group select {
            flex: 1;
            min-width: 200px;
            padding: 10px 15px;
            border: 2px solid #32CD32;
            border-radius: 12px;
            font-size: 14px;
            background: linear-gradient(to bottom, #fff, #f9f9f9);
            transition: border-color 0.3s ease, box-shadow 0.3s ease, background-color 0.3s ease, transform 0.2s ease;
            box-sizing: border-box;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }
        .form-group input[type="text"]:hover,
        .form-group textarea:hover,
        .form-group select:hover {
            background: #fff;
            transform: translateY(-2px);
        }
        .form-group input[type="text"]:focus,
        .form-group textarea:focus,
        .form-group select:focus {
            border-color: #FFA500;
            box-shadow: 0 0 12px rgba(255, 165, 0, 0.5);
            background: #fff5e6;
            transform: translateY(-2px);
            outline: none;
        }
        .form-group textarea {
            resize: vertical;
            min-height: 90px;
        }
        .form-group input[type="file"] {
            flex: 1;
            min-width: 200px;
            padding: 8px;
            border: 2px dashed #32CD32;
            border-radius: 12px;
            background: linear-gradient(to bottom, #f9f9f9, #f0f0f0);
            font-size: 13px;
            transition: border-color 0.3s ease, background-color 0.3s ease, transform 0.2s ease;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }
        .form-group input[type="file"]:hover {
            border-color: #FFA500;
            background: #fff5e6;
            transform: translateY(-2px);
        }
        .form-group select {
            appearance: none;
            background: url('data:image/svg+xml;utf8,<svg fill="%2332CD32" height="24" viewBox="0 0 24 24" width="24" xmlns="http://www.w3.org/2000/svg"><path d="M7 10l5 5 5-5z"/></svg>') no-repeat right 15px center, linear-gradient(to bottom, #fff, #f9f9f9);
            background-size: 18px;
            padding-right: 35px;
        }
        .form-group .btn {
            margin-left: 135px;
        }
        .btn {
            background: linear-gradient(to bottom, #FFA500, #FF8C00);
            color: white;
            border: none;
            border-radius: 20px;
            padding: 10px 20px;
            font-weight: 600;
            font-size: 14px;
            cursor: pointer;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
        }
        .btn:hover {
            background: linear-gradient(to bottom, #32CD32, #28a745);
            transform: scale(1.05);
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
        }
        .btn::before {
            content: '🐾';
            margin-right: 8px;
            font-size: 16px;
            opacity: 0.8;
        }
        .text-danger {
            font-size: 14px;
            color: #FF4500;
            text-align: center;
            display: block;
            margin-bottom: 20px;
            font-weight: 500;
            background-color: rgba(255, 69, 0, 0.1);
            padding: 8px;
            border-radius: 6px;
        }
        .paw-decoration {
            position: absolute;
            font-size: 30px;
            opacity: 0.3;
            color: #32CD32;
            pointer-events: none;
        }
        .paw-1 { top: 20px; left: 40px; transform: rotate(20deg); }
        .paw-2 { bottom: 20px; right: 40px; transform: rotate(-30deg); }
        .paw-3 { top: 50%; left: -10px; transform: rotate(45deg); }
        .paw-4 { bottom: 50%; right: -10px; transform: rotate(-45deg); }
        .floating-paw {
            position: fixed;
            font-size: 35px;
            color: #FFA500;
            opacity: 0.15;
            pointer-events: none;
            animation: float 8s infinite ease-in-out;
        }
        @keyframes float {
            0%, 100% { transform: translateY(0) rotate(0deg); }
            50% { transform: translateY(-15px) rotate(5deg); }
        }
        .cat, .dog {
            position: fixed;
            font-size: 40px;
            z-index: 10;
            display: none;
        }
        .cat {
            bottom: 30px;
            left: -50px;
            animation: chaseCat 4s linear forwards;
        }
        .dog {
            bottom: 30px;
            left: -100px;
            animation: chaseDog 4s linear forwards;
        }
        @keyframes chaseCat {
            0% { left: -50px; transform: rotate(0deg); }
            100% { left: 100vw; transform: rotate(360deg); }
        }
        @keyframes chaseDog {
            0% { left: -100px; transform: rotate(0deg); }
            100% { left: calc(100vw - 50px); transform: rotate(-360deg); }
        }
        .form-group textarea.hakkimizda-textarea {
            min-height: 150px;
        }
        .chart-container {
            max-width: 400px;
            margin: 20px auto;
        }
        @media (max-width: 992px) {
            .sidebar {
                width: 70px;
                padding: 20px 5px;
            }
            .sidebar h2 {
                font-size: 16px;
                margin-bottom: 20px;
            }
            .sidebar a {
                padding: 10px;
                font-size: 0;
                justify-content: center;
            }
            .sidebar-left a::before {
                margin: 0;
                font-size: 20px;
            }
            .main-content {
                margin-left: 70px;
                padding: 20px;
            }
            .stats {
                grid-template-columns: 1fr;
            }
            .stat-box {
                width: 100%;
            }
            .form-group {
                flex-direction: column;
                align-items: flex-start;
            }
            .form-group label {
                text-align: left;
                margin-bottom: 8px;
                flex: 0 0 auto;
            }
            .form-group input[type="text"],
            .form-group textarea,
            .form-group select,
            .form-group input[type="file"] {
                min-width: 100%;
            }
            .form-group .btn {
                margin-left: 0;
            }
            .chart-container {
                max-width: 100%;
            }
        }
        @media (max-width: 576px) {
            .main-content {
                margin-left: 20px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="sidebar sidebar-left">
                <h2>PetShop Admin</h2>
                <a href="WebForm8.aspx" class='<%= Request.Url.AbsolutePath.Contains("WebForm8.aspx") ? "active" : "" %>'>Ana Sayfa</a>
                <a href="WebForm6.aspx" class='<%= Request.Url.AbsolutePath.Contains("WebForm6.aspx") ? "active" : "" %>'>Dashboard</a>
                <a href="Hakkimizda.aspx" class='<%= Request.Url.AbsolutePath.Contains("Hakkimizda.aspx") ? "active" : "" %>'>Hakkımızda</a>
                <a href="#urunler">Ürünler</a>
                <a href="#siparisler">Siparişler</a>
                <a href="#mesajlar">Mesajlar</a>
                <a href="#kullanicilar">Kullanıcılar</a>
                <a href="#kategoriler">Kategoriler</a>
                <a href="#markalar">Markalar</a>
                <a href="#hakkimizda-duzenle">Hakkımızda Düzenle</a>
                <a href="Close.aspx">Çıkış Yap</a>
            </div>

            <div class="main-content">
                <h1>Admin Paneli</h1>
                <asp:Label ID="lblMesaj" runat="server" CssClass="text-danger"></asp:Label>

                <div class="stats">
                    <div class="stat-box">
                        <asp:Label ID="lblSiparisSayisi" runat="server"></asp:Label>
                    </div>
                    <div class="stat-box">
                        <asp:Label ID="lblToplamGelir" runat="server"></asp:Label>
                    </div>
                    <div class="stat-box">
                        <asp:Label ID="lblKullaniciSayisi" runat="server"></asp:Label>
                    </div>
                </div>

                <h2 id="siparisler">Siparişler</h2>
                <asp:GridView ID="gvSiparisler" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="SiparisID" OnRowDeleting="gvSiparisler_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="SiparisID" HeaderText="Sipariş ID" ReadOnly="True" />
                        <asp:BoundField DataField="MusteriAdi" HeaderText="Müşteri Adı" ReadOnly="True" />
                        <asp:BoundField DataField="UrunAdi" HeaderText="Ürün Adı" ReadOnly="True" />
                        <asp:BoundField DataField="Adet" HeaderText="Adet" ReadOnly="True" />
                        <asp:BoundField DataField="SiparisTarihi" HeaderText="Sipariş Tarihi" ReadOnly="True" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="command-button" />
                    </Columns>
                </asp:GridView>

                <h2 id="mesajlar">Mesajlar</h2>
                <asp:GridView ID="gvMesajlar" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="MesajID"
                    OnRowCommand="gvMesajlar_RowCommand" OnRowDeleting="gvMesajlar_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="MesajID" HeaderText="Mesaj ID" ReadOnly="True" />
                        <asp:BoundField DataField="AdSoyad" HeaderText="Ad Soyad" ReadOnly="True" />
                        <asp:BoundField DataField="Eposta" HeaderText="E-posta" ReadOnly="True" />
                        <asp:BoundField DataField="Konu" HeaderText="Konu" ReadOnly="True" />
                        <asp:BoundField DataField="Mesaj" HeaderText="Mesaj" ReadOnly="True" />
                        <asp:BoundField DataField="Tarih" HeaderText="Tarih" ReadOnly="True" DataFormatString="{0:dd.MM.yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Durum">
                            <ItemTemplate>
                                <asp:Label ID="lblOkundu" runat="server" Text='<%# (bool)Eval("Okundu") ? "Okundu" : "Okunmadı" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="İşlemler">
                            <ItemTemplate>
                                <asp:Button ID="btnToggleRead" runat="server" CommandName="ToggleRead" CommandArgument='<%# Eval("MesajID") %>'
                                    Text='<%# (bool)Eval("Okundu") ? "Okunmadı Yap" : "Okundu Yap" %>'
                                    CssClass='<%# (bool)Eval("Okundu") ? "unread-button" : "read-button" %>' />
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("MesajID") %>'
                                    Text="Sil" CssClass="command-button" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <h2>Ürün Ekle</h2>
                <div class="form-group">
                    <label>Ürün Adı:</label>
                    <asp:TextBox ID="txtUrunAdi" runat="server" Placeholder="Ürün adını girin"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Alış Fiyatı:</label>
                    <asp:TextBox ID="txtAlisFiyati" runat="server" Placeholder="Alış fiyatını girin (örn: 50.00)"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Satış Fiyatı:</label>
                    <asp:TextBox ID="txtFiyat" runat="server" Placeholder="Satış fiyatını girin (örn: 99.99)"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Açıklama:</label>
                    <asp:TextBox ID="txtAciklama" runat="server" TextMode="MultiLine" Placeholder="Ürün açıklamasını girin"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Stok:</label>
                    <asp:TextBox ID="txtStok" runat="server" Placeholder="Stok miktarını girin"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Kategori:</label>
                    <asp:DropDownList ID="ddlKategoriler" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Marka:</label>
                    <asp:DropDownList ID="ddlMarkalar" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Ürün Türü:</label>
                    <asp:DropDownList ID="ddlUrunTuru" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Renk:</label>
                    <asp:DropDownList ID="ddlRenk" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Resim:</label>
                    <asp:FileUpload ID="fuResim" runat="server" />
                </div>
                <div class="form-group">
                    <asp:Button ID="btnUrunEkle" runat="server" Text="Ürün Ekle" CssClass="btn" OnClick="btnUrunEkle_Click" />
                </div>

                <h2 id="urunler">Ürünler</h2>
                <asp:GridView ID="gvUrunler" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="UrunID" 
                    OnRowEditing="gvUrunler_RowEditing" OnRowCancelingEdit="gvUrunler_RowCancelingEdit" 
                    OnRowUpdating="gvUrunler_RowUpdating" OnRowDeleting="gvUrunler_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="UrunID" HeaderText="Ürün ID" ReadOnly="True" />
                        <asp:BoundField DataField="UrunAdi" HeaderText="Ürün Adı" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Alış Fiyatı">
                            <ItemTemplate>
                                <asp:Label ID="lblAlisFiyati" runat="server" Text='<%# Eval("AlisFiyati") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtYeniAlisFiyati" runat="server" Text='<%# Bind("AlisFiyati") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Satış Fiyatı">
                            <ItemTemplate>
                                <asp:Label ID="lblFiyat" runat="server" Text='<%# Eval("Fiyat") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtYeniFiyat" runat="server" Text='<%# Bind("Fiyat") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Aciklama" HeaderText="Açıklama" ReadOnly="True" />
                        <asp:BoundField DataField="Stok" HeaderText="Stok" ReadOnly="True" />
                        <asp:BoundField DataField="KategoriAdi" HeaderText="Kategori" ReadOnly="True" />
                        <asp:BoundField DataField="MarkaAdi" HeaderText="Marka" ReadOnly="True" />
                        <asp:BoundField DataField="UrunTuru" HeaderText="Ürün Türü" ReadOnly="True" />
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="command-button" />
                    </Columns>
                </asp:GridView>

                <h2>Ürün Fiyatları Grafiği</h2>
                <div class="chart-container">
                    <canvas id="priceChart"></canvas>
                </div>

                <h2 id="kullanicilar">Kullanıcılar</h2>
                <asp:GridView ID="gvKullanicilar" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="id" 
                    OnRowDeleting="gvKullanicilar_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="Kullanıcı ID" ReadOnly="True" />
                        <asp:BoundField DataField="isimsoyisim" HeaderText="Ad Soyad" ReadOnly="True" />
                        <asp:BoundField DataField="telefonno" HeaderText="Telefon Numarası" ReadOnly="True" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="command-button" />
                    </Columns>
                </asp:GridView>

                <h2 id="kategoriler">Kategoriler</h2>
                <div class="form-group">
                    <label>Kategori Adı:</label>
                    <asp:TextBox ID="txtKategoriAdi" runat="server" Placeholder="Kategori adını girin"></asp:TextBox>
                    <asp:Button ID="btnKategoriEkle" runat="server" Text="Kategori Ekle" CssClass="btn" OnClick="btnKategoriEkle_Click" />
                </div>
                <asp:GridView ID="gvKategoriler" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="KategoriID" 
                    OnRowDeleting="gvKategoriler_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="KategoriID" HeaderText="Kategori ID" ReadOnly="True" />
                        <asp:BoundField DataField="KategoriAdi" HeaderText="Kategori Adı" ReadOnly="True" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="command-button" />
                    </Columns>
                </asp:GridView>

                <h2 id="markalar">Markalar</h2>
                <div class="form-group">
                    <label>Marka Adı:</label>
                    <asp:TextBox ID="txtMarkaAdi" runat="server" Placeholder="Marka adını girin"></asp:TextBox>
                    <asp:Button ID="btnMarkaEkle" runat="server" Text="Marka Ekle" CssClass="btn" OnClick="btnMarkaEkle_Click" />
                </div>
                <asp:GridView ID="gvMarkalar" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="MarkaID" 
                    OnRowDeleting="gvMarkalar_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="MarkaID" HeaderText="Marka ID" ReadOnly="True" />
                        <asp:BoundField DataField="MarkaAdi" HeaderText="Marka Adı" ReadOnly="True" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="command-button" />
                    </Columns>
                </asp:GridView>

                <h2 id="hakkimizda-duzenle">Hakkımızda Düzenle</h2>
                <div class="form-group">
                    <label>Giriş Metni:</label>
                    <asp:TextBox ID="txtGiris" runat="server" TextMode="MultiLine" CssClass="hakkimizda-textarea" Placeholder="Giriş metnini girin"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Biz Kimiz?:</label>
                    <asp:TextBox ID="txtBizKimiz" runat="server" TextMode="MultiLine" CssClass="hakkimizda-textarea" Placeholder="Biz Kimiz? metnini girin"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Misyonumuz:</label>
                    <asp:TextBox ID="txtMisyon" runat="server" TextMode="MultiLine" CssClass="hakkimizda-textarea" Placeholder="Misyon metnini girin"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Hizmetlerimiz:</label>
                    <asp:TextBox ID="txtHizmetler" runat="server" TextMode="MultiLine" CssClass="hakkimizda-textarea" Placeholder="Hizmetler metnini girin"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnHakkimizdaGuncelle" runat="server" Text="Hakkımızda Güncelle" CssClass="btn" OnClick="btnHakkimizdaGuncelle_Click" />
                </div>

                <div class="paw-decoration paw-1">🐾</div>
                <div class="paw-decoration paw-2">🐾</div>
                <div class="paw-decoration paw-3">🐾</div>
                <div class="paw-decoration paw-4">🐾</div>
                <div class="floating-paw" style="top: 20%; left: 10%;">🐾</div>
                <div class="floating-paw" style="top: 60%; right: 15%; animation-delay: 2s;">🐾</div>
                <div class="cat">🐱</div>
                <div class="dog">🐶</div>
            </div>
        </div>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
        <script>
            document.querySelectorAll('.sidebar a[href^="#"]').forEach(anchor => {
                anchor.addEventListener('click', function (e) {
                    e.preventDefault();
                    const targetId = this.getAttribute('href').substring(1);
                    const targetElement = document.getElementById(targetId);
                    if (targetElement) {
                        targetElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
                    }
                });
            });

            const cat = document.querySelector('.cat');
            const dog = document.querySelector('.dog');
            setInterval(() => {
                cat.style.display = cat.style.display === 'none' ? 'block' : 'none';
                dog.style.display = dog.style.display === 'none' ? 'block' : 'none';
            }, 4000);

            window.onload = function () {
                var ctx = document.getElementById('priceChart').getContext('2d');
                new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: ['Ortalama Alış Fiyatı', 'Ortalama Satış Fiyatı', 'Ortalama Kâr'],
                        datasets: [{
                            data: [<%= ViewState["AvgPurchasePrice"] ?? "0" %>, <%= ViewState["AvgSalePrice"] ?? "0" %>, <%= ViewState["AvgProfit"] ?? "0" %>],
                            backgroundColor: ['#28a745', '#dc3545', '#007bff'],
                            borderColor: ['#218838', '#c82333', '#0056b3'],
                            borderWidth: 1
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
                                labels: {
                                    font: { size: 14, family: 'Arial, sans-serif' },
                                    color: '#333'
                                }
                            },
                            tooltip: {
                                callbacks: {
                                    label: function (context) {
                                        return context.label + ': ' + context.parsed.toFixed(2) + ' TL';
                                    }
                                }
                            }
                        }
                    }
                });
            };
        </script>
    </form>
</body>
</html>