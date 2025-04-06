<template>
  <div>
    {{name}}
  </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'

export default class CreateUpdateBook extends Vue {
  name!: string | null
  author!: string | null
  yearPublication!: number
  countCopies!: number
  isUpdate!: boolean

  constructor () {
    super()
    this.isUpdate = false
  }

  mounted (): void {
    if (this.$store.getters.selectedBook) {
      this.isUpdate = true
      this.name = this.$store.getters.selectedBookOption.name
      // this.author = this.$store.getters.selectedBook.author
      // this.yearPublication = this.$store.getters.selectedBook.yearPublication
      // this.countCopies = this.$store.getters.selectedBook.countCopies
    }
  }
}
</script>
