<script setup lang="ts">
import { computed, type HTMLAttributes } from 'vue'
import { Primitive, type PrimitiveProps } from 'radix-vue'
import { type ButtonVariants, buttonVariants } from '.'
import { cn } from '@/lib/utils'

interface Props extends PrimitiveProps {
  variant?: ButtonVariants['variant']
  size?: ButtonVariants['size']
  class?: HTMLAttributes['class']
}

const props = withDefaults(defineProps<Props>(), {
  as: 'button',
})

const buttonStyle = computed(() => {
  if (props.variant === 'destructive') {
    return "background-image: linear-gradient(to right, #ff0000, #ff0000);"
  } else {
    return "background-image: conic-gradient(#000 0% 12.5%, #ff0000 12.5% 25%, #000 25% 37.5%, #ff0000 37.5% 50%, #000 50% 62.5%, #ff0000 62.5% 75%, #000 75% 87.5%, #ff0000 87.5% 100%);"
  }
})
</script>

<template>
  <Primitive :as="as" :as-child="asChild" :class="cn(buttonVariants({ variant, size }), props.class)"
    :style="buttonStyle">
    <slot />
  </Primitive>
</template>
