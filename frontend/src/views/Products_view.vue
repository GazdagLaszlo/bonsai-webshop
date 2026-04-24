<template>
    <div class="mt-6">
        <section class="section">
            <div class="columns">
                
                <div class="column is-half">
                    <router-link to="/products/indoor-bonsai-trees">
                        <div class="is-clickable image-container" @mouseover="hovered = 1" @mouseleave="hovered = null">
                            <figure class="image is-4by2">
                                <img src="@/assets/images/indoor_bonsai.jpg" alt="" class="curved">
                            </figure>
                            <div class="hover-text" :class="{ 'visible': hovered === 1 }">
                                <b class="is-size-5">Beltéri bonsai fák</b><br><br>
                                <p>Hozd be a természet szépségét otthonodba bonsai fáinkkal! Ezek a kompakt, elegáns növények tökéletesen illeszkednek bármely szoba hangulatához, és a megfelelő gondozás mellett hosszú évekig díszítik környezetedet.</p>                            
                            </div>
                        </div>
                    </router-link>
                </div>

                <div class="column is-half">
                    <router-link to="/products/accessories">
                        <div class="is-clickable image-container" @mouseover="hovered = 2" @mouseleave="hovered = null">
                            <figure class="image is-4by2">
                                <img src="@/assets/images/accessories.jpg" alt="" class="curved">
                            </figure>
                            <div class="hover-text" :class="{ 'visible': hovered === 2 }">
                                <b class="is-size-5">Kellékek</b><br><br>
                                <p>Tedd még szebbé és könnyebbé a bonsai gondozást prémium kellékeinkkel! Minőségi eszközeink segítenek a fák formázásában, öntözésében és ápolásában, így a bonsai művészete könnyedén elsajátítható és élvezhető.</p>
                            </div>
                        </div>
                    </router-link>
                </div>

            </div>
        </section>
    </div>
    <router-view></router-view>
</template>
    
<script setup lang="ts">
import { ref } from 'vue';
const hovered = ref<number | null>(null);
</script>

<style scoped>
.image-container {        
    position: relative;
    display: block;
    overflow: hidden;
    border-radius: 8px;
}

.image-container img {
    width: 100%;
    display: block;
    transition: transform 0.3s ease;
}

.image-container:hover img {
    transform: scale(1.03);
}

.hover-text {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    background: rgba(0, 0, 0, 0.6);
    color: white;
    text-align: center;
    padding: 12px 16px;
    transition: height 0.3s ease;
    height: 50px;
    overflow: hidden;
}

.hover-text.visible {
    height: 100%;
}
</style>