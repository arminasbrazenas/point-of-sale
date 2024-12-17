import { create } from 'zustand';

export interface ApplicationUser {
  id: number;
  businessId: number | null;
  role: string;
}

interface AppState {
  applicationUser: ApplicationUser | null;
  setApplicationUser: (applicationUser: ApplicationUser | null) => void;
  updateApplicationUser: (updates: Partial<ApplicationUser>) => void; 
  resetApplicationUser: () => void; 
}

export const useAppStore = create<AppState>((set) => ({
  applicationUser: null,

  setApplicationUser: (applicationUser) =>
    set((state) => ({
      applicationUser: applicationUser ? { ...applicationUser } : null, 
    })),

  updateApplicationUser: (updates) =>
    set((state) => ({
      applicationUser: state.applicationUser
        ? { ...state.applicationUser, ...updates } 
        : null,
    })),

  resetApplicationUser: () =>
    set(() => ({
      applicationUser: null,
    })),
}));