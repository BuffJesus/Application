// Decompiled with JetBrains decompiler
// Type: FableMod.CLRCore.ProgressInterface
// Assembly: FableMod.CLRCore, Version=1.0.4918.423, Culture=neutral, PublicKeyToken=null
// MVID: 9BFDF4CA-2166-4C71-B7DE-FD9072E9B599
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.CLRCore.dll

using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.CLRCore;

public abstract class ProgressInterface : IDisposable
{
  protected Collection<ProgressInterface.Item> m_Items = new Collection<ProgressInterface.Item>();
  protected float m_Current;

  private void \u007EProgressInterface()
  {
    int index = 0;
    if (0 < this.m_Items.Count)
    {
      do
      {
        if (this.m_Items[index] is IDisposable disposable)
          disposable.Dispose();
        ++index;
      }
      while (index < this.m_Items.Count);
    }
    this.m_Items.Clear();
    if (!(this.m_Items is IDisposable items))
      return;
    items.Dispose();
  }

  public void Initialize()
  {
    int index = 0;
    if (0 < this.m_Items.Count)
    {
      do
      {
        if (this.m_Items[index] is IDisposable disposable)
          disposable.Dispose();
        ++index;
      }
      while (index < this.m_Items.Count);
    }
    this.m_Items.Clear();
    this.m_Current = 0.0f;
    this.SetValue(0.0f);
  }

  public void Begin(int steps)
  {
    if (this.m_Items.Count == 0)
    {
      this.m_Items.Add(new ProgressInterface.Item()
      {
        m_Start = 0.0f,
        m_End = 1f,
        m_Step = 1f / (float) steps
      });
    }
    else
    {
      float step = this.m_Items[this.m_Items.Count - 1].m_Step;
      this.m_Items.Add(new ProgressInterface.Item()
      {
        m_Start = this.m_Current,
        m_End = this.m_Current + step,
        m_Step = step / (float) steps
      });
    }
  }

  public void Update()
  {
    float num = this.m_Items[this.m_Items.Count - 1].m_Step + this.m_Current;
    this.m_Current = num;
    if ((double) num > (double) this.m_Items[this.m_Items.Count - 1].m_End)
    {
      ProgressInterface progressInterface = this;
      progressInterface.m_Current = progressInterface.m_Items[this.m_Items.Count - 1].m_End;
    }
    ProgressInterface progressInterface1 = this;
    progressInterface1.SetValue(progressInterface1.m_Current);
  }

  public void End()
  {
    if (this.m_Items.Count > 0)
    {
      ProgressInterface progressInterface = this;
      progressInterface.m_Current = progressInterface.m_Items[this.m_Items.Count - 1].m_End;
      if (this.m_Items[this.m_Items.Count - 1] is IDisposable disposable)
        disposable.Dispose();
      this.m_Items.RemoveAt(this.m_Items.Count - 1);
    }
    else
      this.m_Current = 1f;
    ProgressInterface progressInterface1 = this;
    progressInterface1.SetValue(progressInterface1.m_Current);
  }

  protected abstract void SetValue(float value);

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EProgressInterface();
    }
    else
    {
      // ISSUE: explicit finalizer call
      this.Finalize();
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  protected class Item
  {
    public float m_Start;
    public float m_End;
    public float m_Step;
  }
}
