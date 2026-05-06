import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home_view.vue";
import Products from "../views/Products_view.vue";
import Accessory from "../views/Accessory_view.vue";
import AccessoriesCategory from "../views/Accessories_category.vue";
import Indoor_Bonsai from "../views/Product_indoor_bonsai_view.vue";
import Care from "../views/Care_view.vue";
import About_Us from "../views/About_us_view.vue";
import Contact from "../views/Contact_view.vue";
import Cart from "../views/Cart_view.vue";
import UserAuthentication from "../views/User_Authentication_view.vue";
import Profile from "../views/Profile_view.vue";
import ProductView from "../views/Product_view.vue";

const routes = [
  { path: "/", name: "Home", component: Home },
  { path: "/products", name: "Products", component: Products },
  {
    path: "/products/indoor-bonsai-trees",
    name: "Indoor_Bonsai",
    component: Indoor_Bonsai,
  },
  {
    path: "/products/indoor-bonsai-trees/:productId",
    name: "ProductView",
    component: ProductView,
  },
  {
    path: "/products/accessories",
    name: "Accessories",
    component: AccessoriesCategory,
  },
  {
    path: "/products/accessories/:category",
    name: "ProductCategory",
    component: Accessory,
  },
  {
    path: "/products/accessories/:category/:productId",
    name: "ProductView",
    component: ProductView,
  },

  { path: "/care", name: "Care", component: Care },
  { path: "/about-us", name: "About-Us", component: About_Us },
  { path: "/contact", name: "Contact", component: Contact },
  { path: "/cart", name: "Cart", component: Cart },
  { path: "/user-authentication", name: "UserAuth", component: UserAuthentication },
  { path: "/account", name: "Account", component: Profile },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
