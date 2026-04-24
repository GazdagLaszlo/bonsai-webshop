import axiosInstance from "./axios.config";
import {
  ProductApi,
  UserApi,
  OrderApi,
  ProductCategoryApi,
  //OrderItemApi,
  //AddressApi,
  Configuration,
} from "../../generated-sources/openapi";
import axios from "axios";

const User = new UserApi(
  new Configuration({ basePath: "" }),
  undefined,
  axiosInstance,
);

const Product = new ProductApi(
  new Configuration({ basePath: "" }),
  undefined,
  axiosInstance,
);

const Order = new OrderApi(
  new Configuration({ basePath: "" }),
  undefined,
  axiosInstance,
);

const ProductCategory = new ProductCategoryApi(
  new Configuration({ basePath: "" }),
  undefined,
  axiosInstance,
);

/*
const OrderItem = new OrderItemApi(new Configuration({ basePath: ''}),
    undefined,
    axiosInstance
);

const Address = new AddressApi(new Configuration({ basePath: ''}),
    undefined,
    axiosInstance
);
*/

const api = {
  User,
  Product,
  Order,
  ProductCategory,
  //OrderItem,
  //Address
};

export default api;
