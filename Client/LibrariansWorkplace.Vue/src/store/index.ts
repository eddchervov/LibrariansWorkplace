import { createStore } from 'vuex'
import { GetIssuedBooksDto } from '@/models/dto/get-issued-books-dto'
import { GetAvailableBooksDto } from '@/models/dto/get-available-books-dto'
import { GetBookDto } from '@/models/dto/get-book-dto'
import { CreateBookDto } from '@/models/dto/create-book-dto'
import { UpdateBookDto } from '@/models/dto/update-book-dto'
import { CreateReaderDto } from '@/models/dto/create-reader-dto'
import { Option } from '@/models/view-models/option'
import { GiveBookToReaderDto } from '@/models/dto/give-book-to-reader-dto'
import { ReturnBookToLibraryDto } from '@/models/dto/return-book-to-library-dto'
import { UpdateReaderVm } from '@/models/view-models/update-reader-vm'
import axios from 'axios'

const serverPath = 'https://localhost:44397'

export default createStore({
  state: {
    issuedBooks: new Array<GetIssuedBooksDto>(),
    availableBooks: new Array<GetAvailableBooksDto>(),
    foundBooks: new Array<GetBookDto>(),
    readerOptions: new Array<Option>(),
    bookOptions: new Array<Option>(),
    selectedBook: null,
    selectedBookOption: null,
    selectedReader: null,
    bookManagementTab: 'list'
  },
  getters: {
    issuedBooks: (state) => state.issuedBooks as GetIssuedBooksDto[],
    availableBooks: (state) => state.availableBooks as GetAvailableBooksDto[],
    selectedBookOption: (state) => state.selectedBookOption as Option | null,
    selectedBook: (state) => state.selectedBook as GetBookDto | null,
    bookOptions: (state) => state.bookOptions as Option[],
    bookManagementTab: (state) => state.bookManagementTab
  },
  mutations: {
    setBookManagementTab: (state, tab) => {
      state.bookManagementTab = tab
    },
    setIssuedBooks: (state, issuedBooks) => {
      state.issuedBooks = issuedBooks
    },

    setAvailableBooks: (state, availableBooks) => {
      state.availableBooks = availableBooks
    },
    setFoundBooks: (state, books) => {
      state.foundBooks = books
    },
    setBookOptions: (state, books) => {
      state.bookOptions = books
    },
    addBookOption: (state, book) => {
      state.bookOptions.push(book)
    },
    setReaderOptions: (state, readers) => {
      state.readerOptions = readers
    },
    addReaderOption: (state, reader) => {
      state.readerOptions.push(reader)
    },
    setSelectedReader: (state, reader) => {
      state.selectedReader = reader
    },
    setSelectedBookOption: (state, book) => {
      state.selectedBookOption = book
    },
    setSelectedBook: (state, book) => {
      state.selectedBook = book
    },
    updateReader: (state, vm) => {
      const reader = state.readerOptions.find((x) => x.id === vm.id)
      if (!reader) return new Error('не найден читатель с id=' + vm.id)
      reader.name = vm.fullName
    },
    deleteReader: (state, readerId) => {
      const reader = state.readerOptions.find((x) => x.id === readerId)
      if (!reader) return new Error('не найден читатель с id=' + readerId)
      const index = state.readerOptions.indexOf(reader)
      if (index !== -1) {
        state.readerOptions.splice(index, 1)
      }
    }
  },
  actions: {
    loadBookManagement ({ commit }) {
      this.dispatch('getBookOptions')
    },
    loadBooksPage ({ commit }) {
      this.dispatch('getIssuedBooks')
      this.dispatch('getAvailableBooks')
    },

    async getBookOptions ({ commit }) {
      axios
        .get(serverPath + '/books/options')
        .then((p) => {
          commit('setBookOptions', p.data)
        })
        .catch((e) => {
          alert('Произошла ошибка получения списка книг')
        })
    },
    async giveBookToReader ({ commit }, dto: GiveBookToReaderDto) {
      axios
        .post(serverPath + '/book-management/give-book-to-reader', dto)
        .then((p) => {
          this.dispatch('getIssuedBooks')
          this.dispatch('getAvailableBooks')
        })
        .catch((e) => {
          alert('Произошла ошибка выдачи книги')
        })
    },
    async returnBookToLibrary ({ commit }, dto: ReturnBookToLibraryDto) {
      axios
        .post(serverPath + '/book-management/return-book-to-library', dto)
        .then((p) => {
          this.dispatch('getIssuedBooks')
          this.dispatch('getAvailableBooks')
        })
        .catch((e) => {
          alert('Произошла ошибка сдачи книги')
        })
    },
    async getReaderById ({ commit }, readerId: number) {
      axios
        .get(serverPath + '/readers/' + readerId)
        .then((p) => {
          commit('setSelectedReader', p.data)
        })
        .catch((e) => {
          alert('Произошла ошибка получения читателя')
        })
    },
    async getReaderOptions ({ commit }) {
      axios
        .get(serverPath + '/readers/options')
        .then((p) => {
          commit('setReaderOptions', p.data)
        })
        .catch((e) => {
          alert('Произошла ошибка получения списка читателей')
        })
    },
    async createReader ({ commit }, dto: CreateReaderDto) {
      axios
        .post(serverPath + '/readers', dto)
        .then((p) => {
          commit('addReaderOption', {
            id: p.data.id,
            name: dto.fullName
          })
        })
        .catch((e) => {
          alert('Произошла ошибка создания читателя')
        })
    },
    async updateReader ({ commit }, vm: UpdateReaderVm) {
      axios
        .put(serverPath + '/readers/' + vm.id, vm.dto)
        .then((p) => {
          commit('updateReader', vm)
        })
        .catch((e) => {
          alert('Произошла ошибка обновления читателя')
        })
    },
    async deleteReader ({ commit }, readerId: number) {
      axios
        .delete(serverPath + '/readers/' + readerId)
        .then((p) => {
          commit('deleteReader', readerId)
        })
        .catch((e) => {
          alert('Произошла ошибка удаления читателя')
        })
    },

    async getBookById  ({ commit }, id: number) {
      axios
        .get(serverPath + '/books/' + id)
        .then((p) => {
          commit('addBookOption', {
            id: p.data.id,
            name: `${dto.name}, ${dto.author} ${dto.yearPublication} г.`
          })
          // по ui не понятно, какие списки и для чего
          this.dispatch('getIssuedBooks')
          this.dispatch('getAvailableBooks')
        })
        .catch((e) => {
          alert('Произошла ошибка создания книги')
        })
    },

    async createBook ({ commit }, dto: CreateBookDto) {
      axios
        .post(serverPath + '/books', dto)
        .then((p) => {
          commit('addBookOption', {
            id: p.data.id,
            name: `${dto.name}, ${dto.author} ${dto.yearPublication} г.`
          })
          // по ui не понятно, какие списки и для чего
          this.dispatch('getIssuedBooks')
          this.dispatch('getAvailableBooks')
        })
        .catch((e) => {
          alert('Произошла ошибка создания книги')
        })
    },
    async updateBook ({ commit }, dto: UpdateBookDto) {
      axios
        .put(serverPath + '/books', dto)
        .then((p) => {
          // по ui не понятно, какие списки и для чего
          this.dispatch('getIssuedBooks')
          this.dispatch('getAvailableBooks')
        })
        .catch((e) => {
          alert('Произошла ошибка обновления книги')
        })
    },
    async deleteBook ({ commit }, bookdId: number) {
      axios
        .delete(serverPath + '/books/' + bookdId)
        .then((p) => {
          // по ui не понятно, какие списки и для чего
          this.dispatch('getIssuedBooks')
          this.dispatch('getAvailableBooks')
        })
        .catch((e) => {
          alert('Произошла ошибка удаления книги')
        })
    },
    async booksSearchByName ({ commit }) {
      axios
        .get(serverPath + '/books/search-by-name')
        .then((p) => {
          commit('setFoundBooks', p.data)
        })
        .catch((e) => {
          alert('Произошла ошибка поиска книг')
        })
    },
    async getIssuedBooks ({ commit }) {
      axios
        .get(serverPath + '/books/issued-books')
        .then((p) => {
          commit('setIssuedBooks', p.data)
        })
        .catch((e) => {
          alert('Произошла ошибка получения списка выданных книг')
        })
    },
    async getAvailableBooks ({ commit }) {
      axios
        .get(serverPath + '/books/available-books')
        .then((p) => {
          commit('setAvailableBooks', p.data)
        })
        .catch((e) => {
          alert('Произошла ошибка получения списка доступных для выдачи книг')
        })
    }
  },
  modules: {}
})
