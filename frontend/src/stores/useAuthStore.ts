import { defineStore } from "pinia";
import { ref, computed } from "vue";
import { useRouter } from "vue-router";
import api from "@/api/api";
import { tokenKeyName } from "@/constants/constants";
import { jwtDecode } from "jwt-decode";
import { UserCreateDTO, UserLoginDTO } from "generated-sources/openapi";

interface JwtPayload {
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
}

export const useAuthStore = defineStore("auth", () => {
  const router = useRouter();

  const token = ref<string | null>(sessionStorage.getItem(tokenKeyName));
  const email = ref<string | null>(null);
  const role = ref<string | null>(null);
  const username = ref<string | null>(null);

  const isLoggedIn = computed(() => !!token.value);

  function decodeToken(accessToken: string) {
    const decoded = jwtDecode<JwtPayload>(accessToken);
    username.value =
      decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    email.value =
      decoded[
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
      ];
    role.value =
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  }

  if (token.value) {
    decodeToken(token.value);
  }

  async function login(form: UserLoginDTO) {
    const res = await api.User.apiUserLoginPost({
      email: form.email,
      password: form.password,
    });
    const accessToken = res.data.accessToken!;
    sessionStorage.setItem(tokenKeyName, accessToken);
    token.value = accessToken;
    decodeToken(accessToken);
    return res;
  }

  async function logout() {
    try {
      await api.User.apiUserLogoutPost();
    } catch (err) {
      console.error(err);
    } finally {
      sessionStorage.removeItem(tokenKeyName);
      token.value = null;
      email.value = null;
      role.value = null;
      username.value = null;
      router.push("/");
    }
  }

  async function register(form: UserCreateDTO) {
    const response = await api.User.apiUserRegisterPost(form);

    if (response.data?.accessToken) {
      const accessToken = response.data.accessToken;
      sessionStorage.setItem(tokenKeyName, accessToken);
      token.value = accessToken;
      decodeToken(accessToken);
    }

    return response;
  }

  return {
    token,
    email,
    role,
    username,
    isLoggedIn,
    login,
    register,
    logout,
  };
});
