<template>
  <div class="my-6">
    <div class="mt-6">
      <hr class="mb-1" />
      <span class="is-size-6"></span
      ><router-link to="/products"
        ><a class="is-size-6">Termékeink /</a></router-link
      ><span class="has-text-weight-semibold"> Kellékek</span>
      <hr class="mb-1 mt-2" />
    </div>
    <!----------------------->
    <div class="my-6 columns section">
      <div
        class="column is-flex is-flex-direction-column is-justify-content-center mb-6 ml-6"
      >
        <p class="is-size-4 mb-3 has-text-weight-bold">
          Kellékek - Gondozás mesterfokon
        </p>
        <p class="has-text-justified">
          Tedd még szebbé és könnyebbé a bonsai gondozást prémium kellékeinkkel.
          Minőségi eszközeink segítenek a formázásban, öntözésben és ápolásban.
        </p>
      </div>

      <div class="column is-one-third">
        <figure class="image">
          <img src="@/assets/images/accessories.jpg" class="curved" />
        </figure>
      </div>
    </div>
    <hr class="mb-1" />
    <p class="is-size-6 mb-4">
      A termékek megtekintéséhez válasszon az alábbi kategóriák közül!
    </p>
    <!----------------------->
    <div class="columns is-multiline mt-6 is-tablet is-flex-wrap-wrap">
      <div
        class="column is-one-quarter"
        v-for="item in accessories"
        :key="item.id"
      >
        <router-link :to="`/products/accessories/${item.urlSlug}`">
          <div class="category-box">
            <figure class="image is-4by2">
              <img
                :src="item.image ?? ''"
                :alt="item.name ?? ''"
                class="curved"
                
              />
            </figure>

            <span class="is-size-5 has-text-weight-medium category-title">
              {{ item.name }}
            </span>
          </div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import api from "@/api/api";
import { computed, onMounted, ref } from "vue";
import { ProductCategoryDTO } from "generated-sources/openapi";

const categories = ref<ProductCategoryDTO[]>([]);
const isLoading = ref(false);

async function getCategories() {
  isLoading.value = true;
  try {
    const res = await api.ProductCategory.apiProductCategoryGetAllGet();
    categories.value = res.data;
  } finally {
    isLoading.value = false;
  }
}

onMounted(getCategories);

const accessories = computed(() => {
  return categories.value.filter((item) => item.type !== 0);
});
</script>

<style scoped>
.category-title {
  display: block;
  width: 100%;
  position: absolute;
  bottom: 0;
  box-sizing: border-box;
  background: rgba(0, 0, 0, 0.6);
  color: white;
  text-align: center;
  padding: 12px 16px;
  transition: height 0.3s ease;
  border-radius: 0px 0px 10px 10px;
}

.category-box {
  position: relative;
}
.category-box:hover .image img {
  transform: scale(1.05);
}
.image {
  overflow: hidden;
  border-radius: 10px;
}
.category-box .image img{
    width: 100%;
    height: 200px;
    object-fit: cover;
    transition: transform 0.3s ease;
}
</style>
