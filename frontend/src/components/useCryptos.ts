import {ref, computed, onMounted, watch} from 'vue'
import axios from 'axios'


interface Crypto {
    coinLoreId: number
    name: string
    symbol: string
    priceUsd: number
    percentChange24h: number
    marketCapUsd: number
}

export function useCryptos() {
    const cryptos = ref<Crypto[]>([])
    const loading = ref(true)
    const error = ref<string | null>(null)
    const filter = ref('')
    const currentPage = ref(1)
    const itemsPerPage = 10
    
    const sortBy = ref<'MarketCapUsd' | 'PercentChange24h' | 'Name'>('MarketCapUsd')
    const sortDirection = ref<'Asc' | 'Desc'>('Desc')


    const filteredCryptos = computed(() =>
        cryptos.value.filter(c =>
            c.name.toLowerCase().includes(filter.value.toLowerCase()) ||
            c.symbol.toLowerCase().includes(filter.value.toLowerCase())
        )
    )

    watch(filter, () => {
        currentPage.value = 1
    })

    const totalPages = computed(() =>
        Math.ceil(filteredCryptos.value.length / itemsPerPage)
    )

    const paginatedCryptos = computed(() => {
        const start = (currentPage.value - 1) * itemsPerPage
        return filteredCryptos.value.slice(start, start + itemsPerPage)
    })

    const selectedSort = ref('marketcap-desc')

    function handleSortChange() {
        switch (selectedSort.value) {
            case 'marketcap-desc':
                fetchCryptos('MarketCapUsd', 'Desc')
                break
            case 'gainers':
                fetchCryptos('PercentChange24h', 'Desc')
                break
            case 'losers':
                fetchCryptos('PercentChange24h', 'Asc')
                break
        }
    }

    async function fetchCryptos(
        sortBy: 'MarketCapUsd' | 'PercentChange24h',
        sortDirection: 'Asc' | 'Desc'
    ) {
        loading.value = true
        error.value = null

        try {
            const res = await axios.get('http://localhost:5158/cryptos', {
                params: {
                    sortBy,
                    direction: sortDirection
                }
            })
            cryptos.value = res.data
        } catch (err: any) {
            error.value = 'Error fetching cryptos'
        } finally {
            loading.value = false
        }
    }

    function nextPage() {
        if (currentPage.value < totalPages.value) {
            currentPage.value++
        }
    }

    function prevPage() {
        if (currentPage.value > 1) {
            currentPage.value--
        }
    }

    const formatNumber = (value: number, large = false) => {
        return large
            ? value.toLocaleString('en-US', { notation: 'compact', compactDisplay: 'short' })
            : value.toFixed(2)
    }

    onMounted(() => {
        fetchCryptos('MarketCapUsd', 'Desc')
    })


    return {
        cryptos,
        filteredCryptos,
        filter,
        loading,
        sortBy,
        sortDirection,
        fetchCryptos,
        currentPage,
        error,
        paginatedCryptos,
        totalPages,
        nextPage,
        prevPage,
        formatNumber,
        handleSortChange,
        selectedSort
    }
}
