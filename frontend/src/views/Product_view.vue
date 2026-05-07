<template>
  <section class="section">
    <div class="container">
      <div class="columns is-variable is-6" v-if="product">
        <div class="column is-half">
          <figure class="image is-4by3">
            <img :src="product?.image ?? ''" :alt="product?.name ?? ''" />
          </figure>
        </div>

        <div class="column is-half">
          <h1 class="title">{{ product.name }}</h1>

          <p class="is-size-4 has-text-weight-semibold mb-4">
            {{ moneyFormat(product.price) }} Ft
          </p>

          <div class="content" style="text-align: justify">
            <p>{{ product.description }}</p>
          </div>

          <div class="column is-flex is-flex-direction-row">
            <button
              @click="decreaseQuantity"
              class="button quantity-setter-button is-size-7"
            >
              -
            </button>
            <p class="has-text-weight-semibold mx-4 py-1">{{ quantity }} db</p>
            <button
              @click="increaseQuantity"
              class="button quantity-setter-button is-size-7"
            >
              +
            </button>
          </div>
          <button
            class="button is-dark is-fullwidth mt-4"
            @click="handleAddToCart(product, quantity)"
          >
            Kosárba
          </button>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import api from "@/api/api";
import { useCartStore } from "@/stores/useCartStore";
import { moneyFormat } from "@/utils/moneyFormat";
import { ProductDTO } from "generated-sources/openapi";
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { useToast } from "vue-toastification";
const route = useRoute();
const cartStore = useCartStore();
const toast = useToast();

const productId = Number(route.params.productId);

const product = ref<ProductDTO>();
const quantity = ref<number>(1);

onMounted(async () => {
  const res = await api.Product.apiProductGetByIdIdGet(productId);
  product.value = res.data;
});

const handleAddToCart = (product: ProductDTO | undefined, quantity: number) => {
  if (!product) return;

  cartStore.addToCart(product, quantity);
  toast.success(`Kosárba helyezve!`, {
    timeout: 2000,
  });
};

const increaseQuantity = () => {
  quantity.value++;
};

const decreaseQuantity = () => {
  if (quantity.value > 1) {
    quantity.value--;
  }
};
</script>

<style scoped>
.image img {
  border-radius: 10px;
  object-fit: contain;
}
</style>
