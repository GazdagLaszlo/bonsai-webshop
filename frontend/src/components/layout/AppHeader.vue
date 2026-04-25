<template>
  <div :style="{ height: '10vh' }">
    <nav
      class="navbar"
      :style="{ backgroundColor: COLORS.background, height: '100%' }"
    >
      <div id="brand" class="p-2">
        <a href="/">
          <p class="is-size-4 has-text-black has-text-weight-bold">
            Bonsai Shop
          </p>
          <p>Minőségi bonsai fák és kellékek</p>
        </a>

        <a
          role="button"
          class="navbar-burger"
          aria-label="menu"
          aria-expanded="false"
          data-target="navbar_to_expand"
        >
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div class="navbar-menu">
        <div class="navbar-end">
          <router-link to="/" class="navbar-item">Kezdőlap</router-link>

          <div class="navbar-item has-dropdown is-hoverable">
            <router-link to="/products" class="navbar-item">
              Termékeink
            </router-link>

            <div class="navbar-dropdown">
              <router-link
                to="/products/indoor-bonsai-trees"
                class="navbar-item"
              >
                Beltéri bonsai fák
              </router-link>

              <hr class="navbar-divider" />

              <router-link to="/products/accessories/pots" class="navbar-item">
                Ültetőedények
              </router-link>
              <router-link to="/products/accessories/bonsai-soils">
                <a class="navbar-item">Ültetőközegek</a>
              </router-link>
              <router-link to="/products/accessories/nutrients">
                <a class="navbar-item">Tápolatok</a>
              </router-link>
              <router-link to="/products/accessories/wires">
                <a class="navbar-item">Drótok</a>
              </router-link>
              <router-link to="/products/accessories/bonsai-scissors">
                <a class="navbar-item">Bonsai ollók</a>
              </router-link>
            </div>
          </div>

          <router-link to="/care" class="navbar-item">Gondozás</router-link>
          <router-link to="/about-us" class="navbar-item">Rólunk</router-link>
          <router-link to="/contact" class="navbar-item">Kapcsolat</router-link>

          <router-link to="/cart" class="navbar-item">
            <img src="@/assets/images/cart_icon.png" class="cart_icon" />
            <p
              class="cart_quantity has-text-weight-bold"
              :style="{
                visibility: cartStore.totalQuantity > 0 ? 'visible' : 'hidden',
              }"
            >
              {{ cartStore.totalQuantity }}
            </p>
          </router-link>
          <div v-if="!authStore.isLoggedIn" class="navbar-item user-auth">
            <router-link to="/user-authentication">
              <span class="icon mr-2">
                <i class="fas fa-user"></i>
              </span>
              Bejelentkezés
            </router-link>
          </div>
          <div v-else class="navbar-item user-auth">
              <router-link to="/account">
              <span class="icon mr-2">
                <i class="fas fa-user"></i>
              </span>
              Fiókom
            </router-link>
          </div>
        </div>
      </div>
    </nav>
  </div>
</template>

<script setup lang="ts">
import COLORS from "@/constants/colors";
import { useAuthStore } from "@/stores/useAuthStore";
import { useCartStore } from "@/stores/useCartStore";
import { RouterLink } from "vue-router";

const cartStore = useCartStore();
const authStore = useAuthStore();
</script>

<style scoped>
.cart_quantity {
  position: relative;
  top: -10px;
  left: -10px;
  font-size: 10px;
  background-color: black;
  color: white;
  padding: 3px 8px;
  border-radius: 50px;
}

.cart_icon {
  width: 18px;
}
.navbar-item:hover {
  background-color: transparent;
  color: var(--color-link);
  font-weight: bold;
}
.navbar-item {
  border-radius: 10px;
}
.navbar-item.is-active,
.navbar-item.router-link-exact-active {
  background-color: transparent !important;
  font-weight: bold;
}
.user-auth{
  border-left: 1px solid black;
  border-radius: 0;
  padding-left: 20px;
}
</style>
