// vite.config.ts
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "file:///C:/Users/chuo1/source/repos/WinWheel/Frontend/vue-clientApp/node_modules/.pnpm/vite@5.3.1_@types+node@20.14.2/node_modules/vite/dist/node/index.js";
import vue from "file:///C:/Users/chuo1/source/repos/WinWheel/Frontend/vue-clientApp/node_modules/.pnpm/@vitejs+plugin-vue@5.0.5_vite@5.3.1_@types+node@20.14.2__vue@3.4.29_typescript@5.4.5_/node_modules/@vitejs/plugin-vue/dist/index.mjs";
import vueJsx from "file:///C:/Users/chuo1/source/repos/WinWheel/Frontend/vue-clientApp/node_modules/.pnpm/@vitejs+plugin-vue-jsx@3.1.0_vite@5.3.1_@types+node@20.14.2__vue@3.4.29_typescript@5.4.5_/node_modules/@vitejs/plugin-vue-jsx/dist/index.mjs";
import tailwind from "file:///C:/Users/chuo1/source/repos/WinWheel/Frontend/vue-clientApp/node_modules/.pnpm/tailwindcss@3.4.4/node_modules/tailwindcss/lib/index.js";
import autoprefixer from "file:///C:/Users/chuo1/source/repos/WinWheel/Frontend/vue-clientApp/node_modules/.pnpm/autoprefixer@10.4.19_postcss@8.4.38/node_modules/autoprefixer/lib/autoprefixer.js";
var __vite_injected_original_import_meta_url = "file:///C:/Users/chuo1/source/repos/WinWheel/Frontend/vue-clientApp/vite.config.ts";
var vite_config_default = defineConfig({
  css: {
    postcss: {
      plugins: [tailwind(), autoprefixer()]
    }
  },
  plugins: [
    vue(),
    vueJsx()
  ],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", __vite_injected_original_import_meta_url))
    }
  }
});
export {
  vite_config_default as default
};
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcudHMiXSwKICAic291cmNlc0NvbnRlbnQiOiBbImNvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9kaXJuYW1lID0gXCJDOlxcXFxVc2Vyc1xcXFxjaHVvMVxcXFxzb3VyY2VcXFxccmVwb3NcXFxcV2luV2hlZWxcXFxcRnJvbnRlbmRcXFxcdnVlLWNsaWVudEFwcFwiO2NvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9maWxlbmFtZSA9IFwiQzpcXFxcVXNlcnNcXFxcY2h1bzFcXFxcc291cmNlXFxcXHJlcG9zXFxcXFdpbldoZWVsXFxcXEZyb250ZW5kXFxcXHZ1ZS1jbGllbnRBcHBcXFxcdml0ZS5jb25maWcudHNcIjtjb25zdCBfX3ZpdGVfaW5qZWN0ZWRfb3JpZ2luYWxfaW1wb3J0X21ldGFfdXJsID0gXCJmaWxlOi8vL0M6L1VzZXJzL2NodW8xL3NvdXJjZS9yZXBvcy9XaW5XaGVlbC9Gcm9udGVuZC92dWUtY2xpZW50QXBwL3ZpdGUuY29uZmlnLnRzXCI7aW1wb3J0IHsgZmlsZVVSTFRvUGF0aCwgVVJMIH0gZnJvbSAnbm9kZTp1cmwnXG5cbmltcG9ydCB7IGRlZmluZUNvbmZpZyB9IGZyb20gJ3ZpdGUnXG5pbXBvcnQgdnVlIGZyb20gJ0B2aXRlanMvcGx1Z2luLXZ1ZSdcbmltcG9ydCB2dWVKc3ggZnJvbSAnQHZpdGVqcy9wbHVnaW4tdnVlLWpzeCdcbmltcG9ydCB0YWlsd2luZCBmcm9tIFwidGFpbHdpbmRjc3NcIlxuaW1wb3J0IGF1dG9wcmVmaXhlciBmcm9tIFwiYXV0b3ByZWZpeGVyXCJcblxuLy8gaHR0cHM6Ly92aXRlanMuZGV2L2NvbmZpZy9cbmV4cG9ydCBkZWZhdWx0IGRlZmluZUNvbmZpZyh7XG4gIGNzczoge1xuICAgIHBvc3Rjc3M6IHtcbiAgICAgIHBsdWdpbnM6IFt0YWlsd2luZCgpLCBhdXRvcHJlZml4ZXIoKV0sXG4gICAgfSxcbiAgfSxcbiAgcGx1Z2luczogW1xuICAgIHZ1ZSgpLFxuICAgIHZ1ZUpzeCgpLFxuICBdLFxuICByZXNvbHZlOiB7XG4gICAgYWxpYXM6IHtcbiAgICAgICdAJzogZmlsZVVSTFRvUGF0aChuZXcgVVJMKCcuL3NyYycsIGltcG9ydC5tZXRhLnVybCkpXG4gICAgfVxuICB9XG59KVxuXG4iXSwKICAibWFwcGluZ3MiOiAiO0FBQW1YLFNBQVMsZUFBZSxXQUFXO0FBRXRaLFNBQVMsb0JBQW9CO0FBQzdCLE9BQU8sU0FBUztBQUNoQixPQUFPLFlBQVk7QUFDbkIsT0FBTyxjQUFjO0FBQ3JCLE9BQU8sa0JBQWtCO0FBTm9OLElBQU0sMkNBQTJDO0FBUzlSLElBQU8sc0JBQVEsYUFBYTtBQUFBLEVBQzFCLEtBQUs7QUFBQSxJQUNILFNBQVM7QUFBQSxNQUNQLFNBQVMsQ0FBQyxTQUFTLEdBQUcsYUFBYSxDQUFDO0FBQUEsSUFDdEM7QUFBQSxFQUNGO0FBQUEsRUFDQSxTQUFTO0FBQUEsSUFDUCxJQUFJO0FBQUEsSUFDSixPQUFPO0FBQUEsRUFDVDtBQUFBLEVBQ0EsU0FBUztBQUFBLElBQ1AsT0FBTztBQUFBLE1BQ0wsS0FBSyxjQUFjLElBQUksSUFBSSxTQUFTLHdDQUFlLENBQUM7QUFBQSxJQUN0RDtBQUFBLEVBQ0Y7QUFDRixDQUFDOyIsCiAgIm5hbWVzIjogW10KfQo=
