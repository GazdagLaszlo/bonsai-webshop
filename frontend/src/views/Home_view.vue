<template>
    <div class="my-6">

        <div class="hero mb-6 is-large">
            <div class="hero-body main_img" id="hero-body">                
                <p class="title has-text-white is-size-4">A bonsai nem csupán egy fa,<br> hanem egy élet, amit a gondoskodás formál.</p>
            </div>
        </div>

        <p>- Főoldalon bonsai típusok - Metszés szerint</p>
        <p>- Lehetnek esetleg hobbi bonsai fák (citrom, majomkenyérfa)</p>
        <p>- Felhasználói profil(rendelések)</p>
        <P>- Bonsai metszés szerinti típusai, lehetőségek</P>
        <p>- Lábrészben is legyenek feltűntetve a navigációs elemek</p>
        <p>- FAQ - Gyakori kérdések rész (Mennyi víz kell neki? Miért hervad?...)</p>
        <p>- Kosárba elem hozzáadásakor legyen valami indikátor, hogy 1 elem hozzá lett adva</p>

        <br><br>
        <b>Footer</b>
        <p>Facebook és Instagram oldal elérés</p>
        <p>Információk címszó alatt elemek</p>
        <p>- Kapcsolat</p>
        <p>- Adatvédelmi nyilatkozat</p>
        <p>- Általános szerződési feltételek</p>
        <p>- Termékeink feltüntetése</p>
        <br><br>


        <br><br><br><br>
        <p>Legyenek bemutatva a bonsai típusok, majd lehessen átnavigálni a kínálatban lévő típusokra.</p>            
    </div>
</template>

<script>
    export default {
        name: "Home_view"
    };
</script>

<style>
    .main_img{
        background-image: url("../assets/images/pauline-bernfeld-hBYo2tCR6Wc-unsplash.jpg");
        background-repeat: no-repeat;
        background-size:cover;
        background-position: 60% 20%;
        border-radius: 5px;        
    }
    #hero-body{
        padding: 30rem 5rem 7rem 5rem;
    }
</style>