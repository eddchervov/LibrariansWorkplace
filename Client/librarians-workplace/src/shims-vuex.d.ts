import { Store } from "./router"

declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    $store: Store
  }
}
