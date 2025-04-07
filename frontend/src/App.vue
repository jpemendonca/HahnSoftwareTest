<script setup lang="ts">
import {initFlowbite} from 'flowbite'
import {onMounted} from "vue";
import {useCryptos} from "./components/useCryptos.ts";


const {
  filter,
  loading,
  error,
  paginatedCryptos,
  totalPages,
  nextPage,
  prevPage,
  currentPage,
  formatNumber,
  // sortBy,
  // sortDirection,
  // fetchCryptos,
  selectedSort,
  handleSortChange
} = useCryptos()

onMounted(() => {
  initFlowbite()
})
</script>

<template>
  <div class="min-h-screen bg-white dark:bg-gray-900 p-6 text-gray-900 dark:text-white">
    <div class="max-w-screen-xl mx-auto">
      <div v-if="loading" class="text-center py-6">Loading...</div>
      <div v-else-if="error" class="text-red-500 py-6">{{ error }}</div>

      <div v-else class="card-default mt-3">
        <h1 class="text-2xl font-bold mb-4">Cryptocurrency</h1>

        <div class="flex flex-column sm:flex-row flex-wrap space-y-4 sm:space-y-0 items-center justify-between pb-4">
          
          <select
              v-model="selectedSort"
              @change="handleSortChange"
              class="select w-48"
          >
            <option disabled value="">Order by</option>
            <option value="marketcap-desc">Market Cap (Desc)</option>
            <option value="gainers">Top Gainers (24h)</option>
            <option value="losers">Top Losers (24h)</option>
          </select>


          <!--          Search crypto input-->
          <label for="table-search" class="sr-only">Search</label>
          <div class="relative">
            <div class="absolute inset-y-0 left-0 rtl:inset-r-0 rtl:right-0 flex items-center ps-3 pointer-events-none">
              <svg class="w-5 h-5 text-gray-500 dark:text-gray-400"
                   aria-hidden="true"
                   fill="currentColor"
                   viewBox="0 0 20 20"
                   xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd"
                      d="M8 4a4 4 0 100 8 4 4 0 000-8zM2 8a6 6 0 1110.89 3.476l4.817 4.817a1 1 0 01-1.414 1.414l-4.816-4.816A6 6 0 012 8z"
                      clip-rule="evenodd"></path>
              </svg>
            </div>
            <input v-model="filter" type="text" id="table-search" class="search-input"
                   placeholder="Filter by name or symbol">
          </div>
        </div>

        <div class="overflow-x-auto shadow-md sm:rounded-lg">
          <table class="table-base">
            <thead class="thead-base">
            <tr>
              <th scope="col" class="px-6 py-3">Name</th>
              <th scope="col" class="px-6 py-3">Symbol</th>
              <th scope="col" class="px-6 py-3">Price (USD)</th>
              <th scope="col" class="px-6 py-3">% 24h</th>
              <th scope="col" class="px-6 py-3">Market Cap</th>
            </tr>
            </thead>
            <tbody>
            <tr
                v-for="crypto in paginatedCryptos"
                :key="crypto.coinLoreId"
                class="tr-hover"
            >
              <td class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap dark:text-white">
                {{ crypto.name }}
              </td>
              <td class="px-6 py-4">
                {{ crypto.symbol }}
              </td>
              <td class="px-6 py-4">
                ${{ formatNumber(crypto.priceUsd) }}
              </td>
              <td
                  class="px-6 py-4 font-semibold"
                  :class="crypto.percentChange24h >= 0 ? 'text-green-500' : 'text-red-500'"
              >
                {{ crypto.percentChange24h }}%
              </td>
              <td class="px-6 py-4">
                {{ formatNumber(crypto.marketCapUsd, true) }}
              </td>
            </tr>
            </tbody>
          </table>
        </div>

        <div class="flex justify-between items-center mt-4">
          <button @click="prevPage" :disabled="currentPage === 1" class="button-pagination disabled:opacity-50">
            Previous
          </button>

          <span class="text-sm text-gray-600 dark:text-gray-300">
    Page {{ currentPage }} of {{ totalPages }}
  </span>

          <button @click="nextPage" :disabled="currentPage === totalPages"
                  class="button-pagination disabled:opacity-50">
            Next
          </button>
        </div>


      </div>

    </div>
  </div>
</template>

