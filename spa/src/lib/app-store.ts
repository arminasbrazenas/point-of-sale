import { create } from 'zustand';

export interface ApplicationUser {
    id: number;
    businessId: number | null;
    role: string;
}

interface AppState {
    applicationUser: ApplicationUser | null;
    setApplicationUser: (applicationUser: ApplicationUser | null) => void;
}

export const useAppStore = create<AppState>((set) => ({
    applicationUser: null,
    setApplicationUser: (applicationUser) => set({ applicationUser }),
  }));