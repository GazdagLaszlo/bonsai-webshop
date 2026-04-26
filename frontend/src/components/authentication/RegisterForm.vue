<template>
  <form @submit.prevent="handleRegister" class="register-form">
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
      <label class="label">Teljes név</label>
      <div class="control has-icons-left">
        <input
          v-model="form.name"
          class="input"
          type="text"
          placeholder="Kiss Péter"
          required
        />
        <span class="icon is-small is-left">
          <i class="fas fa-user"></i>
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

    <div class="field">
      <label class="label">Jelszó megerősítése</label>
      <div class="control has-icons-left">
        <input
          v-model="form.confirmPassword"
          class="input"
          type="password"
          placeholder="********"
          required
        />
        <span class="icon is-small is-left">
          <i class="fas fa-check-double"></i>
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
          Regisztráció
        </button>
      </div>
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref, reactive } from "vue";
import { useAuthStore } from "@/stores/useAuthStore";
import { useToast } from "vue-toastification";
import { UserCreateDTO } from "generated-sources/openapi";
import { useRouter } from "vue-router";
import { translateError } from "@/utils/errorTranslator";

const authStore = useAuthStore();
const toast = useToast();
const router = useRouter();

const loading = ref(false);
const form = reactive({
  email: "",
  name: "",
  password: "",
  confirmPassword: "",
});

const resetForm = () => {
  form.email = "";
  form.name = "";
  form.password = "";
  form.confirmPassword = "";
};

const handleRegister = async () => {
  if (form.password !== form.confirmPassword) {
    toast.error("A megadott jelszavak nem egyeznek!");
    return;
  }
  if (form.password.length < 8) {
    toast.error("A jelszónak legalább 8 karakter hosszúnak kell lennie!");
    return;
  }
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!emailRegex.test(form.email)) {
    toast.error("Helytelen email címet adott meg!");
    return;
  }

  loading.value = true;

  try {
    const userCreateDTO: UserCreateDTO = {
      name: form.name.trim(),
      email: form.email.trim(),
      password: form.password,
    };

    await authStore.register(userCreateDTO);
    toast.success("Sikeres regisztráció!");

    router.push("/");
  } catch (error: any) {
    toast.error(translateError(error));
  } finally {
    loading.value = false;
    resetForm();
  }
};
</script>
