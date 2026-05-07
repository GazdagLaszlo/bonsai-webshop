<template>
  <div class="my-6">
    <div class="hero mb-6 is-large">
      <div class="hero-body main_img hero-body">
        <p class="title has-text-white is-size-4">
          A bonsai nem csupán egy fa,<br />
          hanem egy élet, amit a gondoskodás formál.
        </p>
      </div>
    </div>

    <section class="mb-6">
      <p class="is-size-5 has-text-weight-medium mb-1">
        Bonsai stílusok metszés szerint
      </p>
      <p class="is-size-6 mb-5" :style="{ color: COLORS.secondaryText }">
        A bonsai formáját a metszési stílus határozza meg – ismerd meg a
        legfontosabbakat.
      </p>
      <div class="columns is-multiline">
        <div
          class="column is-one-quarter"
          v-for="style in bonsaiStyles"
          :key="style.name"
        >
          <div class="card curved" style="overflow: hidden">
            <div>
              <img :src="style.image" :alt="style.name" class="card-img" />
            </div>
            <div class="p-4">
              <p class="has-text-weight-medium is-size-6">{{ style.name }}</p>
              <p
                class="is-italic is-size-7 pb-2"
                :style="{ color: COLORS.lightBrown }"
              >
                {{ style.japanese }}
              </p>
              <p
                class="is-size-7 has-text-justified"
                :style="{ color: COLORS.secondaryText }"
              >
                {{ style.description }}
              </p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <hr class="mb-6" />

    <section class="mb-6">
      <p class="is-size-5 has-text-weight-medium mb-1">Hobbi bonsai fák</p>
      <p class="is-size-6 mb-5" :style="{ color: COLORS.secondaryText }">
        Különleges, gyümölcstermő és egzotikus fajták kezdőknek és haladóknak
        egyaránt.
      </p>
      <div class="columns is-multiline">
        <div
          class="column is-one-third"
          v-for="tree in hobbyBonsaiTrees"
          :key="tree.name"
        >
          <div class="card curved" style="overflow: hidden">
            <div>
              <img :src="tree.image" :alt="tree.name" class="hobby-card-img" />
            </div>
            <div class="p-4">
              <span
                class="tag mb-2"
                :style="{
                  backgroundColor: COLORS.background,
                  color: COLORS.darkBrown,
                }"
                >{{ tree.tag }}</span
              >
              <p class="has-text-weight-medium is-size-6 pb-2">
                {{ tree.name }}
              </p>
              <p class="is-size-7 has-text-justified mb-3">
                {{ tree.description }}
              </p>
              <router-link
                to="/products/indoor-bonsai"
                :style="{ color: COLORS.darkBrown }"
                class="is-size-7 hobby-link"
              >
                Megnézem →
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </section>

    <hr class="mb-6" />

    <section class="mb-6">
      <p class="is-size-5 has-text-weight-medium mb-1">Gyakori kérdések</p>
      <p class="is-size-6 mb-5" :style="{ color: COLORS.secondaryText }">
        A legtöbbet feltett kérdések bonsai gondozásról.
      </p>
      <div class="faq-list">
        <div
          class="faq-item"
          v-for="(faq, index) in faqs"
          :key="index"
          @click="toggleFaq(index)"
        >
          <div
            class="faq-question is-flex is-justify-content-space-between is-align-items-center pl-4 p-3 is-size-6 has-text-weight-medium"
          >
            <span>{{ faq.question }}</span>
            <span
              class="faq-arrow is-size-5"
              :class="{ open: openFaq === index }"
              >›</span
            >
          </div>
          <div class="faq-answer is-size-7" :style="{color: COLORS.darkBrown}" :class="{ visible: openFaq === index }">
            {{ faq.answer }}
          </div>
        </div>
      </div>
    </section>

    <p>- Felhasználói profil(rendelések)</p>
    <p>- Lábrészben is legyenek feltüntetve a navigációs elemek</p>

    <br />
    <b>Footer</b>
    <p>Facebook és Instagram oldal elérés</p>
    <p>Információk címszó alatt elemek</p>
    <p>- Kapcsolat</p>
    <p>- Adatvédelmi nyilatkozat</p>
    <p>- Általános szerződési feltételek</p>
    <p>- Termékeink feltüntetése</p>    
  </div>
</template>

<script setup lang="ts">
import COLORS from "@/constants/colors";
import { ref } from "vue";
import { bonsaiStyles } from "@/data/bonsaiStyles";
import { hobbyBonsaiTrees } from "@/data/hobbyBonsaiTrees";
import { faqs } from "@/data/faqs";

const openFaq = ref<number | null>(null);

function toggleFaq(index: number) {
  openFaq.value = openFaq.value === index ? null : index;
}
</script>

<style scoped>
.main_img {
  background-image: url("../assets/images/pauline-bernfeld-hBYo2tCR6Wc-unsplash.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  background-position: 60% 20%;
  border-radius: 10px;
}
.hero-body {
  padding: 30rem 5rem 7rem 5rem !important;
}
.card-img {
  width: 100%;
  height: 140px;
  object-fit: cover;
}
.hobby-card-img {
  width: 100%;
  height: 180px;
  overflow: hidden;
  object-fit: cover;
}
.hobby-link:hover {
  text-decoration: underline;
}
.faq-list {
  border: 0.5px solid var(--color-line);
  border-radius: 10px;
}
.faq-item {
  border-bottom: 0.5px solid var(--color-line);
  cursor: pointer;
}
.faq-item:last-child {
  border-bottom: none;
}
.faq-arrow {
  color: var(--color-lightBrown);
  transition: transform 0.2s ease;
}
.faq-arrow.open {
  transform: rotate(90deg);
}
.faq-answer {
  max-height: 0;
  overflow: hidden;    
  padding: 0 1.25rem;
  transition:
    max-height 0.3s ease,
    padding 0.3s ease;
}
.faq-answer.visible {
  max-height: 200px;
  padding: 0 1.25rem 1rem;
}
</style>
