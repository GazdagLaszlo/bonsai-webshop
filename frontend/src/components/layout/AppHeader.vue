<template>
  <div :style="{height: '10vh'}">
    <nav class="navbar" :style="{backgroundColor: COLORS.background, height: '100%'}">
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

      <div class="navbar-menu" id="navbar_to_expand">
        <div class="navbar-end">
          <router-link to="/" class="navbar-item">Kezdőlap</router-link>

          <div class="navbar-item has-dropdown is-hoverable">
            <router-link to="/products" class="navbar-item"
              >Termékeink</router-link
            >

            <div class="navbar-dropdown">
              <router-link to="/products/indoor_bonsai">
                <a class="navbar-item">Beltéri bonsai fák</a>
              </router-link>

              <hr class="navbar-divider" />

              <a class="navbar-item">Ültetőedények</a>
              <a class="navbar-item">Ültetőközegek</a>
              <a class="navbar-item">Tápolatok</a>
              <a class="navbar-item">Drótok</a>
              <a class="navbar-item">Bonsai ollók</a>
            </div>
          </div>

          <router-link to="/care" class="navbar-item">Gondozás</router-link>
          <router-link to="/about-us" class="navbar-item">Rólunk</router-link>
          <router-link to="/contact" class="navbar-item"
            >Kapcsolat</router-link
          >

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
        </div>
      </div>
    </nav>
  </div>
</template>

<script setup lang="ts">
import COLORS from "@/constants/colors";
import { useCartStore } from "@/stores/useCartStore";
const cartStore = useCartStore();
</script>

<style scoped>
  .cart_quantity{
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
  .navbar-item:hover{
      background-color: transparent;
      color: var(--color-link);
      font-weight: bold;
  }
  .navbar-item{
      border-radius: 10px;
  }
  .navbar-item.is-active, .navbar-item.router-link-exact-active{
    background-color: transparent !important;
    font-weight: bold;
  }   
</style>