<template>
  <div class="is-flex is-justify-content-center py-5">
    <div class="auth-container card" :style="{width: '40vw', backgroundColor: COLORS.box }">
      <header class="card-header">
        <p class="card-header-title">
          {{ isLogin ? "Bejelentkezés" : "Regisztráció" }}
        </p>
      </header>

      <div class="card-content">
        <LoginForm v-if="isLogin" />
        <RegisterForm v-else />

        <hr />

        <button @click="isLogin = !isLogin" class="button is-text is-fullwidth">
          {{
            isLogin
              ? "Nincs még fiókom? Regisztrálok"
              : "Már van fiókom? Belépek"
          }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import LoginForm from "@/components/authentication/LoginForm.vue";
import RegisterForm from "@/components/authentication/RegisterForm.vue";
import COLORS from "@/constants/colors";

const isLogin = ref(true);
</script>
