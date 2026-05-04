<template>
  <div class="my-6">
    <div class="mt-6">
      <hr class="mb-1" />
      <router-link to="/products"
        ><a class="is-size-6">Termékeink /</a></router-link
      ><router-link to="/products/accessories"
        ><a class="is-size-6"> Kellékek / </a></router-link
      ><span class="has-text-weight-semibold">{{ category?.name }}</span>
      <hr class="mb-1 mt-2" />
    </div>
    <!----------------------->
    <div class="my-6 columns section">
      <div
        class="column is-flex is-flex-direction-column is-justify-content-center mb-6 ml-6"
      >
        <p class="is-size-4 mb-3 has-text-weight-bold">
          {{ category?.name }}
        </p>
        <p class="has-text-justified">
          {{ category?.description }}
        </p>
      </div>

      <div class="column is-one-third">
        <figure class="image i">
          <img :src="category?.image ?? ''" class="curved" />
        </figure>
      </div>
    </div>
    <hr class="line mb-1" />
    <p class="is-size-6 mb-4">
      Bővebb információért válasszon termékeink közül!  
    </p>
    <!----------------------->
    <div class="columns is-multiline mt-6 is-tablet is-flex-wrap-wrap">
      <div
        class="column is-one-quarter"
        v-for="product in products"
        :key="product.id"
      >
        <router-link :to="{name: 'ProductItem', params: {category: product.category?.urlSlug, productId: product.id}}">
          <div class="">
            <p class="has-text-weight-semibold">{{ product.name }}</p>
            <hr class="line mb-3 mt-1" />
          </div>
          <div class="card-image">
            <figure class="image is-5by4">
              <img :src="product.image ?? ''" />
            </figure>
          </div>
        </router-link>
        <div
          class="mt-5 is-flex is-flex-direction-column is-align-items-center"
        >
          <p class="mb-3 has-text-weight-semibold">{{ product.price }} Ft</p>
          <button
            @click="cartStore.addToCart(product, 1)"
            class="button is-normal is-fullwidth"
          >
            Kosárba
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import api from "@/api/api";
import { ProductCategoryDTO, ProductDTO } from "generated-sources/openapi";
import { onMounted, ref, watch } from "vue";
import { useRoute } from "vue-router";
import { useCartStore } from "@/stores/useCartStore";

const route = useRoute();
const products = ref<ProductDTO[]>([]);
const category = ref<ProductCategoryDTO>();
const isLoading = ref(false);

const cartStore = useCartStore();

async function getProducts() {
  isLoading.value = true;
  try {
    if (route.params.category !== null) {
      const res = await api.Product.apiProductGetAllGet(
        route.params.category.toString(),
      );
      products.value = res.data;
    }
  } finally {
    isLoading.value = false;
  }
}

async function getCategory() {
  isLoading.value = true;
  try {
    if (route.params.category !== null) {
      const res =
        await api.ProductCategory.apiProductCategoryGetByUrlSlugUrlSlugGet(
          route.params.category.toString(),
        );
      category.value = res.data;
    }
  } finally {
    isLoading.value = false;
  }
}

onMounted(getProducts);
onMounted(getCategory);

watch(
  () => route.params.category,
  (newUrlSlug) => {
    if (newUrlSlug) {
      getProducts();
      getCategory();
    }
  },
);
</script>

<style scoped>
.image img {
  object-fit: cover;
  border-radius: 10px;
}
</style>
