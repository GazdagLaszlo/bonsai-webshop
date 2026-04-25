<template>
    <div id="app" class="container is-fluid" :style="{backgroundColor: COLORS.background}">
        <AppHeader/>
        
        <main :style="{minHeight: '55vh'}">
            <router-view/>
        </main>        

        <AppFooter/>
    </div>
</template>

<script setup>
import AppFooter from './components/layout/AppFooter.vue';
import AppHeader from './components/layout/AppHeader.vue'
import COLORS from './constants/colors';
/*
onMounted(() => {
    cartStore.loadCart();
});
*/
</script>