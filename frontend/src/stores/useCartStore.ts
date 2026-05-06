import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import type { ProductDTO } from '../../generated-sources/openapi';

type cartItem = ProductDTO & {
    quantity: number;
}

export const useCartStore = defineStore('cart', () => {
    const items = ref<cartItem[]>([]);
    const isOpen = ref<boolean>(false);    

    const totalItems = computed(() => items.value.length);

    const totalQuantity = computed(() => items.value.reduce((sum, item) => sum + (item.quantity ?? 0), 0))
    
    const totalPrice = computed(() =>
        items.value.reduce((sum, item) => sum + ((item.price ?? 0) * (item.quantity ?? 0)), 0)
    );

    function addToCart(product : ProductDTO, quantity: number) {
        const existing = items.value.find(i => i.id === product.id);
        if (existing) {
            existing.quantity = existing.quantity+quantity;
        } else {
            items.value.push({ ...product, quantity: quantity });
        }
    }

    function increaseItemQuantity(productId: number) {
        const item = items.value.find(i => i.id === productId);
        if (item) item.quantity++;
    }

    function decreaseItemQuantity(productId: number) {
        const item = items.value.find(i => i.id === productId);
        if (item) {
            if (item.quantity > 1) item.quantity--;
            else removeFromCart(productId);
        }
    }

    function removeFromCart(productId : number) {
        items.value = items.value.filter(i => i.id !== productId);
    }

    function clearCart() {
        items.value = [];
    }

    return { 
        items, 
        isOpen,
        totalItems, 
        totalPrice,
        totalQuantity,
        addToCart, 
        removeFromCart, 
        clearCart,
        increaseItemQuantity,
        decreaseItemQuantity,
    };
}, {
    persist: true
});