<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hakkimizda.aspx.cs" Inherits="petshopint2.Hakkimizda" %>
<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>PetShop Otomasyonu - Hakkımızda</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <style>
        body {
            background: linear-gradient(135deg, #32CD32, #FFA500);
            font-family: 'Arial', sans-serif;
            color: #333;
            min-height: 100vh;
            margin: 0;
            overflow-x: hidden;
            position: relative;
        }
        body::before {
            content: '';
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('https://www.transparenttextures.com/patterns/paws.png') repeat;
            opacity: 0.15;
            z-index: -1;
        }
        .navbar {
            background: linear-gradient(to right, #FFA500, #FF8C00);
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
        }
        .navbar-brand {
            color: white !important;
            font-weight: 700;
            font-size: 1.5rem;
            display: flex;
            align-items: center;
        }
        .navbar-brand::before {
            content: '🐾';
            margin-right: 8px;
            font-size: 1.2rem;
        }
        .nav-link {
            color: white !important;
            font-weight: 500;
            transition: color 0.3s ease, transform 0.3s ease;
        }
        .nav-link:hover {
            color: #32CD32 !important;
            transform: scale(1.1);
        }
        .container {
            max-width: 900px;
            margin: 50px auto;
            padding: 30px;
            background-color: rgba(255, 255, 255, 0.98);
            border-radius: 15px;
            box-shadow: 0 0 25px rgba(0, 0, 0, 0.2);
            position: relative;
            animation: fadeIn 1s ease-in-out;
        }
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
        h1 {
            color: #FFA500;
            text-align: center;
            font-weight: 700;
            font-size: 2.5rem;
            margin-bottom: 30px;
            text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.1);
        }
        h2 {
            color: #32CD32;
            font-size: 1.8rem;
            font-weight: 600;
            margin-top: 40px;
            margin-bottom: 15px;
            border-bottom: 3px solid #FFA500;
            padding-bottom: 8px;
            position: relative;
            transition: color 0.3s ease;
        }
        h2::before {
            content: '🐾';
            position: absolute;
            left: -30px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 1.5rem;
            color: #FFA500;
            opacity: 0.7;
        }
        .content {
            font-size: 1.1rem;
            line-height: 1.8;
            color: #333;
            background: #f9f9f9;
            padding: 15px;
            border-radius: 10px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }
        .content:hover {
            transform: translateY(-5px);
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.15);
        }
        .error {
            color: #FF4500;
            text-align: center;
            font-weight: 500;
            font-size: 1rem;
            margin-bottom: 20px;
            background: rgba(255, 69, 0, 0.1);
            padding: 10px;
            border-radius: 8px;
        }
        .btn {
            background: linear-gradient(to bottom, #FFA500, #FF8C00);
            color: white;
            border: none;
            border-radius: 25px;
            padding: 12px 25px;
            font-weight: 600;
            font-size: 1rem;
            cursor: pointer;
            display: inline-flex;
            align-items: center;
            text-decoration: none;
            margin-top: 30px;
            transition: background 0.3s ease, transform 0.3s ease, box-shadow 0.3s ease;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
        }
        .btn:hover {
            background: linear-gradient(to bottom, #32CD32, #28a745);
            transform: scale(1.05);
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
        }
        .btn::before {
            content: '🐾';
            margin-right: 8px;
            font-size: 1.2rem;
        }
        .footer {
            background: linear-gradient(to right, #FFA500, #FF8C00);
            color: white;
            text-align: center;
            padding: 20px 0;
            position: relative;
            bottom: 0;
            width: 100%;
            margin-top: 50px;
            box-shadow: 0 -2px 5px rgba(0, 0, 0, 0.2);
        }
        .footer p {
            margin: 0;
            font-size: 0.9rem;
            font-weight: 500;
        }
        .paw-decoration {
            position: absolute;
            font-size: 2rem;
            color: #32CD32;
            opacity: 0.3;
            pointer-events: none;
            z-index: 0;
        }
        .paw-1 { top: 20px; left: 20px; transform: rotate(20deg); }
        .paw-2 { bottom: 20px; right: 20px; transform: rotate(-30deg); }
        .floating-paw {
            position: fixed;
            font-size: 2.5rem;
            color: #FFA500;
            opacity: 0.2;
            pointer-events: none;
            animation: float 8s infinite ease-in-out;
        }
        @keyframes float {
            0%, 100% { transform: translateY(0) rotate(0deg); }
            50% { transform: translateY(-20px) rotate(10deg); }
        }
        .pet-animation {
            position: fixed;
            font-size: 2.5rem;
            z-index: 10;
            opacity: 0.8;
        }
        .cat {
            bottom: 20px;
            left: -50px;
            animation: chaseCat 15s linear infinite;
        }
        .dog {
            bottom: 20px;
            left: -100px;
            animation: chaseDog 15s linear infinite 2s;
        }
        @keyframes chaseCat {
            0% { left: -50px; transform: rotate(0deg); }
            100% { left: 100vw; transform: rotate(360deg); }
        }
        @keyframes chaseDog {
            0% { left: -100px; transform: rotate(0deg); }
            100% { left: calc(100vw - 50px); transform: rotate(-360deg); }
        }
        @media (max-width: 768px) {
            .container {
                margin: 20px;
                padding: 20px;
            }
            h1 {
                font-size: 2rem;
            }
            h2 {
                font-size: 1.5rem;
            }
            .content {
                font-size: 1rem;
                padding: 10px;
            }
            .btn {
                padding: 10px 20px;
                font-size: 0.9rem;
            }
            .navbar-nav {
                text-align: center;
            }
        }
        @media (max-width: 576px) {
            .navbar-brand {
                font-size: 1.2rem;
            }
            .nav-link {
                font-size: 0.9rem;
            }
            .paw-decoration {
                font-size: 1.5rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
        <nav class="navbar navbar-expand-lg">
            <div class="container-fluid">
                <a class="navbar-brand" href="WebForm1.aspx">PetShopInt2</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="WebForm1.aspx">Ana Sayfa</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link active" href="Hakkimizda.aspx">Hakkımızda</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="WebForm6.aspx">Admin Paneli</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Main Content -->
        <div class="container">
            <h1>Hakkımızda</h1>
            <div class="error">
                <asp:Literal ID="litError" runat="server"></asp:Literal>
            </div>

            <h2 id="giris">Giriş</h2>
            <div class="content">
                <asp:Literal ID="litGiris" runat="server"></asp:Literal>
            </div>

            <h2 id="bizkimiz">Biz Kimiz?</h2>
            <div class="content">
                <asp:Literal ID="litBizKimiz" runat="server"></asp:Literal>
            </div>

            <h2 id="misyon">Misyonumuz</h2>
            <div class="content">
                <asp:Literal ID="litMisyon" runat="server"></asp:Literal>
            </div>

            <h2 id="hizmetler">Hizmetlerimiz</h2>
            <div class="content">
                <asp:Literal ID="litHizmetler" runat="server"></asp:Literal>
            </div>

            <a href="WebForm1.aspx" class="btn">Ana Sayfaya Dön</a>

            <!-- Decorative Elements -->
            <div class="paw-decoration paw-1">🐾</div>
            <div class="paw-decoration paw-2">🐾</div>
        </div>

        <!-- Footer -->
        <footer class="footer">
            <p>© 2025 PetShopInt2. Tüm hakları saklıdır. 🐶🐱</p>
        </footer>

        <!-- Floating Decorations -->
        <div class="floating-paw" style="top: 20%; left: 10%;">🐾</div>
        <div class="floating-paw" style="top: 60%; right: 15%; animation-delay: 2s;">🐾</div>
        <div class="pet-animation cat">🐱</div>
        <div class="pet-animation dog">🐶</div>
    </form>

    <!-- JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <script>
        // Smooth Scroll for Navigation
        document.querySelectorAll('a[href^="#"]').forEach(anchor => {
            anchor.addEventListener('click', function (e) {
                e.preventDefault();
                const targetId = this.getAttribute('href').substring(1);
                const targetElement = document.getElementById(targetId);
                if (targetElement) {
                    targetElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
                }
            });
        });

        // Fade-In Animation for Content Sections
        document.addEventListener('DOMContentLoaded', () => {
            const contents = document.querySelectorAll('.content');
            contents.forEach((content, index) => {
                setTimeout(() => {
                    content.style.opacity = '1';
                    content.style.transform = 'translateY(0)';
                }, index * 200);
            });
        });

        // Toggle Pet Animations
        const cat = document.querySelector('.cat');
        const dog = document.querySelector('.dog');
        setInterval(() => {
            cat.style.display = cat.style.display === 'none' ? 'block' : 'none';
            dog.style.display = dog.style.display === 'none' ? 'block' : 'none';
        }, 15000);
    </script>
</body>
</html>