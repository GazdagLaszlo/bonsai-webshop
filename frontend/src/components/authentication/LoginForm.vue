<template>
  <form @submit.prevent="handleLogin" class="login-form">
    <div class="field">
      <label class="label">Email cím</label>
      <div class="control has-icons-left">
        <input
          v-model="form.email"
          class="input"
          type="email"
          placeholder="pelda@email.com"
          required
        />
        <span class="icon is-small is-left">
          <i class="fas fa-envelope"></i>
        </span>
      </div>
    </div>

    <div class="field">
      <label class="label">Jelszó</label>
      <div class="control has-icons-left">
        <input
          v-model="form.password"
          class="input"
          type="password"
          placeholder="********"
          required
        />
        <span class="icon is-small is-left">
          <i class="fas fa-lock"></i>
        </span>
      </div>
    </div>

    <div class="field mt-5">
      <div class="control">
        <button
          class="button is-fullwidth"
          :class="{ 'is-loading': loading }"
          type="submit"
        >
          Bejelentkezés
        </button>
      </div>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue';
import { useAuthStore } from '@/stores/useAuthStore';
import { useToast } from 'vue-toastification';
import { useRouter } from 'vue-router';
import { UserLoginDTO } from 'generated-sources/openapi';

const authStore = useAuthStore();
const toast = useToast();
const router = useRouter();

const loading = ref(false);
const form = reactive({
  email: '',
  password: '',
});

const handleLogin = async () => {
  loading.value = true;
  try {
    const userLoginDTO: UserLoginDTO = {
      email: form.email.trim(),
      password: form.password,
    };

    await authStore.login(userLoginDTO);

    toast.success("Sikeres bejelentkezés!");

    router.push('/');
  } catch (error : any) {
    const message = error.response?.data?.message || "Helytelen adatok!";
    toast.error(message);
  } finally {
    loading.value = false;
  }
};
</script>
