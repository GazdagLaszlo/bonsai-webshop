<template>
    <div id="app" class="container is-fluid" :style="{backgroundColor: COLORS.background}">
        <AppHeader/>
        
        <main :style="{minHeight: '55vh'}">
            <router-view/>
        </main>

        <footer class="pb-5 px-0" :style="{height: '30vh'}">
            <strong class="is-size-5">Bonsai Shop</strong>
            <hr class="has-background-black line mt-2">

            <div class="is-flex is-flex-direction-row columns">
                <div class="column is-half">
                    <p>Teszt Fejlesztő</p>
                    <p>1111 Város, Teszt utca 3.</p>
                    <p>Adószám: 92748282-6-62</p>
                </div>

                <div class="column is-half">
                    <div class="is-flex is-flex-direction-row mb-1">
                        <figure class="image is-24x24">
                            <img src="">
                        </figure>
                        <p class="ml-3">+36 30 837 9264</p>
                    </div>

                    <div class="is-flex is-flex-direction-row">
                        <figure class="image is-24x24">
                            <img src="">
                        </figure>
                        <p class="ml-3">email.cim@gmail.com</p>
                    </div>
                </div>
            </div>

            <div class="mt-5">
                <strong class="column has-text-centered is-size-6">
                    Minden jog fenntartva © 2026 Bonsai Shop
                </strong>
            </div>
        </footer>
    </div>
</template>

<script setup>
import AppHeader from './components/layout/AppHeader.vue'
import COLORS from './constants/colors';
/*
onMounted(() => {
    cartStore.loadCart();
});
*/
</script>