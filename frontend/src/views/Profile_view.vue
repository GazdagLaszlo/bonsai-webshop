<template>
  <div class="mx-6">
    <section class="section">
      <div
        class="is-flex is-flex-direction-column is-align-items-center"
        style="min-height: 80vh"
      >
        <div style="width: 40vw">
          <div class="is-flex is-align-items-center is-flex-direction-column">
            <span class="mb-4">
              <i class="fa-regular fa-circle-user fa-5x"></i>
            </span>

            <div class="p-1 is-size-7 tag role-tag">
              <p>{{ authStore.role }}</p>
            </div>

            <p class="is-size-4 has-text-weight-bold">
              {{ authStore.username }}
            </p>
            <p style="color: grey">{{ authStore.email }}</p>

            <button class="button mt-4" @click="updateModalOpen = true">
              Profil szerkesztése
            </button>

            <hr
              class="has-background-grey-light mt-4"
              style="width: 80%; height: 0.5px"
            />

            <div
              class="is-flex is-justify-content-center mt-2 is-flex-direction-column is-align-items-center"
              style="width: 100%; gap: 5px"
            >
              <div
                class="profile-action-item"
                @click="changePasswordModalOpen = true"
              >
                <span class="icon mr-2">
                  <i class="fa-solid fa-key"></i>
                </span>
                <span>Jelszó módosítása</span>
              </div>

              <div class="profile-action-item" @click="authStore.logout">
                <span class="icon mr-2 has-text-danger">
                  <i class="fas fa-sign-out-alt"></i>
                </span>
                <span class="has-text-danger">Kijelentkezés</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="modal" :class="{ 'is-active': changePasswordModalOpen }">
        <div
          class="modal-background"
          @click="changePasswordModalOpen = false"
        ></div>
        <div class="modal-card">
          <header class="modal-card-head">
            <p class="modal-card-title">Jelszó módosítása</p>
            <button
              class="delete"
              @click="changePasswordModalOpen = false"
            ></button>
          </header>
          <section class="modal-card-body">
            <div class="field">
              <label class="label">Jelenlegi jelszó</label>
              <div class="control">
                <input
                  v-model="passwordForm.currentPassword"
                  class="input"
                  type="password"
                  placeholder="********"
                />
              </div>
            </div>
            <div class="field">
              <label class="label">Új jelszó</label>
              <div class="control">
                <input
                  v-model="passwordForm.newPassword"
                  class="input"
                  type="password"
                  placeholder="********"
                />
              </div>
            </div>
            <div class="field">
              <label class="label">Új jelszó megerősítése</label>
              <div class="control">
                <input
                  v-model="passwordForm.confirmPassword"
                  class="input"
                  type="password"
                  placeholder="********"
                />
              </div>
            </div>
          </section>
          <footer class="modal-card-foot">
            <button class="button" @click="handleChangePassword">Mentés</button>
            <button class="button" @click="changePasswordModalOpen = false">
              Mégse
            </button>
          </footer>
        </div>
      </div>

      <div class="modal" :class="{ 'is-active': updateModalOpen }">
        <div class="modal-background" @click="updateModalOpen = false"></div>
        <div class="modal-card">
          <header class="modal-card-head">
            <p class="modal-card-title">Profil szerkesztése</p>
            <button class="delete" @click="updateModalOpen = false"></button>
          </header>
          <section class="modal-card-body">
            <div class="field">
              <label class="label">Teljes név</label>
              <div class="control">
                <input
                  v-model="updateForm.name"
                  class="input"
                  type="text"
                  :placeholder="authStore.username ?? ''"
                />
              </div>
            </div>
          </section>
          <footer class="modal-card-foot">
            <button class="button" @click="handleUpdateUser">Mentés</button>
            <button class="button" @click="updateModalOpen = false">
              Mégse
            </button>
          </footer>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive } from "vue";
import { useAuthStore } from "@/stores/useAuthStore";
import { useToast } from "vue-toastification";
import api from "@/api/api";
import { ChangePasswordDTO } from "generated-sources/openapi";
import axios from "axios";
import { translateError } from "@/utils/errorTranslator";

const authStore = useAuthStore();
const toast = useToast();

const changePasswordModalOpen = ref(false);
const updateModalOpen = ref(false);

const passwordForm = reactive({
  currentPassword: "",
  newPassword: "",
  confirmPassword: "",
});

const updateForm = reactive({
  name: "",
});

const resetPasswordForm = () => {
  passwordForm.currentPassword = "";
  passwordForm.newPassword = "";
  passwordForm.confirmPassword = "";
};

const resetUpdateForm = () => {
  updateForm.name = "";
};

async function handleChangePassword() {
  if (passwordForm.newPassword !== passwordForm.confirmPassword) {
    toast.error("A két jelszó nem egyezik!");
    return;
  }
  try {
    const passwordDTO: ChangePasswordDTO = {
      oldPassword: passwordForm.currentPassword,
      newPassword: passwordForm.newPassword,
    };

    await api.User.apiUserChangePasswordPut(passwordDTO);
    toast.success("Jelszó sikeresen módosítva!");
    changePasswordModalOpen.value = false;
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      toast.error(translateError(error));
    } else {
      toast.error("Hiba történt a jelszó módosítása során!");
    }
  } finally {
    resetPasswordForm();
  }
}

async function handleUpdateUser() {
  try {
    // await api.User.apiUserUpdatePut({ name: updateForm.name })
    toast.success("Profil sikeresen frissítve!");
    updateModalOpen.value = false;
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      toast.error(translateError(error));
    } else {
      toast.error("Hiba történt a profil frissítése során!");
    }
  } finally {
    resetUpdateForm();
  }
}
</script>

<style scoped>
.profile-action-item {
  display: flex;
  align-items: center;
  padding: 12px;
  border: 1px solid grey;
  width: 80%;
  border-radius: 10px;
  height: 5vh;
  cursor: pointer;
  transition: background 0.2s;
}
.profile-action-item:hover {
  background-color: #f5f5f5;
}
.role-tag {
  background-color: var(--color-background);
  position: relative;
  bottom: 30px;
  margin-bottom: -20px;
}
</style>
