<template>
  <div class="my-6">
    <p class="mb-6 is-size-4 has-text-weight-semibold">Kosár tartalma</p>
    <div class="is-flex is-flex-direction-row">
      <div
        class="column box is-three-fifths p-6 mb-0 mr-4"
        :style="{ backgroundColor: COLORS.box }"
      >
        <ul v-if="cartStore.items.length > 0">
          <li v-for="item in cartStore.items" :key="item.id">
            <div class="columns cart-item mb-5 is-flex is-flex-direction-row">
              <router-link
                :to="{
                  name: item.productCategory.urlSlug == 'indoor-bonsai-trees' ? 'IndoorBonsaiProductView' : 'AccessoriesProductView',
                  params: {
                    productId: item.id,
                    category: item.productCategory.urlSlug,
                  },
                }"
                class="column is-flex is-justify-content-center is-one-fifth"
                style="cursor: pointer"
              >
                <figure class="image is-128x128 is-flex is-align-items-center">
                  <img :src="item.image" />
                </figure>
              </router-link>
              <div class="column is-four-fifths">
                <div
                  class="is-flex is-flex-direction-row is-justify-content-space-between"
                  style="cursor: pointer"
                >                  
                    <router-link
                      :to="{
                        name: item.productCategory.urlSlug == 'indoor-bonsai-trees' ? 'IndoorBonsaiProductView' : 'AccessoriesProductView',
                        params: {
                          productId: item.id,
                          category: item.productCategory.urlSlug,
                        },
                      }"
                      class="has-text-weight-bold"
                    >
                      {{ item.name }}
                    </router-link>                  
                  <figure
                    class="image is-32x32"
                    @click="cartStore.removeFromCart(item.id)"
                  >
                    <img src="@/assets/images/trash_bin.png" alt="" />
                  </figure>
                </div>
                <hr />
                <div
                  class="is-flex is-flex-direction-row is-align-items-space-between"
                >
                  <div class="column is-flex is-flex-direction-row">
                    <button
                      @click="cartStore.decreaseItemQuantity(item.id)"
                      class="button quantity-setter-button is-size-7"
                    >
                      -
                    </button>
                    <p class="has-text-weight-semibold mx-4 py-1">
                      {{ item.quantity }} db
                    </p>
                    <button
                      @click="cartStore.increaseItemQuantity(item.id)"
                      class="button quantity-setter-button is-size-7"
                    >
                      +
                    </button>
                  </div>
                  <div class="column is-flex is-justify-content-end">
                    <p class="has-text-weight-semibold">
                      {{ moneyFormat(item.price * item.quantity) }} Ft
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </li>
        </ul>
        <p v-else>A kosár üres.</p>
      </div>
      <div
        class="column box is-two-fifths p-6"
        :style="{ height: 'fit-content', backgroundColor: COLORS.box }"
      >
        <p class="has-text-weight-bold is-size-5">Rendelés összegzése</p>
        <hr />
        <div
          class="is-flex is-flex-direction-row is-justify-content-space-between"
        >
          <p>Részösszeg</p>
          <p>{{ moneyFormat(cartStore.totalPrice) }} Ft</p>
        </div>
        <div
          class="is-flex is-flex-direction-row is-justify-content-space-between"
        >
          <p>Szállítás</p>
          <p>0 Ft</p>
        </div>
        <hr />
        <div
          class="is-flex is-flex-direction-row is-justify-content-space-between"
        >
          <p class="has-text-weight-bold is-size-5">Végösszeg</p>
          <p class="has-text-weight-bold is-size-5">
            {{ moneyFormat(cartStore.totalPrice) }} Ft
          </p>
        </div>
        <button
          class="button go-to-cart-button mt-6 is-fullwidth"
          v-if="cartStore.items.length > 0"
          @click="sendCartToServer"
        >
          Tovább a pénztárhoz
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { moneyFormat } from "@/utils/moneyFormat";
import { useCartStore } from "@/stores/useCartStore";
import COLORS from "@/constants/colors";

const cartStore = useCartStore();
</script>

<style scoped>
.cart-item {
  border-bottom: 1px solid var(--color-darkBrown);
}
.cart-item img {
  object-fit: contain;
  mix-blend-mode: multiply;
}
.quantity-setter-button {
  border-radius: 5px;
  border: none;
}
</style>
