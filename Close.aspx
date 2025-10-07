<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Close.aspx.cs" Inherits="petshopint2.Close" %>
<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>PetShop Otomasyonu - Çıkış</title>
    <style>
        body {
            background: linear-gradient(135deg, #32CD32, #FFA500);
            font-family: Arial, sans-serif;
            color: #333;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            text-align: center;
        }
        .message {
            background-color: rgba(255, 255, 255, 0.97);
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
        }
        h1 {
            color: #FFA500;
            font-size: 24px;
            margin-bottom: 20px;
        }
        p {
            font-size: 16px;
            color: #333;
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
            text-decoration: none;
            display: inline-block;
            margin-top: 20px;
        }
        .btn:hover {
            background: linear-gradient(to bottom, #32CD32, #28a745);
        }
    </style>
    <script type="text/javascript">
        window.onload = function () {
            try {
                window.close();
                setTimeout(function () {
                    document.getElementById("fallbackMessage").style.display = "block";
                }, 500);
            } catch (e) {
                document.getElementById("fallbackMessage").style.display = "block";
            }
        };
    </script>
</head>
<body>
    <div class="message" id="fallbackMessage" style="display: none;">
        <h1>Çıkış Yapıldı</h1>
        <p>Tarayıcı sekmesini manuel olarak kapatabilirsiniz.</p>
        <a href="WebForm1.aspx" class="btn">Ana Sayfaya Dön</a>
    </div>
</body>
</html>